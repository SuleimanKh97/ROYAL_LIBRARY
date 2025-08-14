import React, { useState, useEffect } from 'react';
import { Card, CardContent, CardHeader, CardTitle } from '../components/ui/card';
import { Button } from '../components/ui/button';
import { Badge } from '../components/ui/badge';
import { Input } from '../components/ui/input';
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '../components/ui/select';
import QuizPlayer from '../components/QuizPlayer';
import PageNav from '../components/PageNav';
import apiService from '../lib/api';
import { showError, showSuccess } from '../lib/sweetAlert';

const QuizzesPage = () => {
  const [quizzes, setQuizzes] = useState([]);
  const [loading, setLoading] = useState(true);
  const [selectedQuiz, setSelectedQuiz] = useState(null);
  const [filters, setFilters] = useState({
    subject: 'all',
    grade: 'all',
    search: ''
  });
  const [subjects, setSubjects] = useState([]);
  const [grades, setGrades] = useState([]);
  const defaultSubjects = [
    'الرياضيات', 'العلوم', 'اللغة العربية', 'اللغة الإنجليزية', 
    'الدراسات الاجتماعية', 'التربية الإسلامية', 'الحاسوب', 'الفنون'
  ];
  const defaultGrades = [
    'الصف الأول', 'الصف الثاني', 'الصف الثالث', 'الصف الرابع', 'الصف الخامس', 'الصف السادس',
    'الصف السابع', 'الصف الثامن', 'الصف التاسع', 'الصف العاشر', 'الصف الحادي عشر', 'الصف الثاني عشر'
  ];

  useEffect(() => {
    fetchQuizzes();
    fetchFilters();
  }, []);

  useEffect(() => {
    fetchQuizzes();
  }, [filters]);

  const fetchQuizzes = async () => {
    try {
      setLoading(true);
      const queryParams = new URLSearchParams();
      if (filters.subject && filters.subject !== 'all') queryParams.append('subject', filters.subject);
      if (filters.grade && filters.grade !== 'all') queryParams.append('grade', filters.grade);
      if (filters.search) queryParams.append('search', filters.search);

      const response = await apiService.apiCall(`/quizzes?${queryParams.toString()}`);
      setQuizzes(response.quizzes || []);
    } catch (error) {
      showError('حدث خطأ أثناء جلب الكويزات');
      setQuizzes([]);
    } finally {
      setLoading(false);
    }
  };

  const fetchFilters = async () => {
    try {
      const [subjectsResponse, gradesResponse] = await Promise.all([
        apiService.apiCall('/quizzes/subjects'),
        apiService.apiCall('/quizzes/grades')
      ]);
      const mergeUnique = (base, extra) => Array.from(new Set([...(base || []), ...(extra || [])]));
      setSubjects(mergeUnique(defaultSubjects, Array.isArray(subjectsResponse) ? subjectsResponse : []));
      setGrades(mergeUnique(defaultGrades, Array.isArray(gradesResponse) ? gradesResponse : []));
    } catch (error) {
      console.error('Error fetching filters:', error);
      setSubjects(defaultSubjects);
      setGrades(defaultGrades);
    }
  };

  const handleStartQuiz = (quiz) => {
    setSelectedQuiz(quiz);
  };

  const handleQuizComplete = (result) => {
    setSelectedQuiz(null);
    // تحديث قائمة الكويزات بعد إكمال الكويز
    fetchQuizzes();
    
    // عرض رسالة نجاح أو فشل
    if (result.isPassed) {
      showSuccess(`مبروك! لقد نجحت في الكويز بنسبة ${result.percentage.toFixed(1)}%`);
    } else {
      showError(`للأسف لم تنجح في الكويز. النسبة المحققة: ${result.percentage.toFixed(1)}%`);
    }
  };

  const handleBackToQuizzes = () => {
    setSelectedQuiz(null);
  };

  const filteredQuizzes = quizzes.filter(quiz => {
    if (filters.search && !quiz.title.toLowerCase().includes(filters.search.toLowerCase())) {
      return false;
    }
    if (filters.subject && filters.subject !== 'all' && quiz.subject !== filters.subject) {
      return false;
    }
    if (filters.grade && filters.grade !== 'all' && quiz.grade !== filters.grade) {
      return false;
    }
    return true;
  });

  if (selectedQuiz) {
    return (
      <div className="container mx-auto px-4 py-8">
        <PageNav />
        <div className="mb-6" />
        <QuizPlayer 
          quizId={selectedQuiz.id} 
          onComplete={handleQuizComplete}
        />
      </div>
    );
  }

  return (
    <div className="container mx-auto px-4 py-8">
      <PageNav />
      <div className="mb-8">
        <h1 className="text-3xl font-bold mb-2 text-gray-900">الكويزات التعليمية</h1>
        <p className="text-gray-800">اختبر معرفتك من خلال مجموعة متنوعة من الكويزات التعليمية</p>
      </div>

      {/* Filters */}
      <div className="mb-6">
        <Card className="border-yellow-200">
          <CardContent className="pt-6">
            <div className="grid grid-cols-1 md:grid-cols-4 gap-4">
              <div>
                <label className="block text-sm font-medium mb-2">البحث</label>
                <Input
                  placeholder="ابحث في الكويزات..."
                  value={filters.search}
                  onChange={(e) => setFilters({ ...filters, search: e.target.value })}
                />
              </div>
              <div>
                <label className="block text-sm font-medium mb-2">المادة</label>
                <Select value={filters.subject} onValueChange={(value) => setFilters({ ...filters, subject: value })}>
                  <SelectTrigger className="border-yellow-200">
                    <SelectValue placeholder="جميع المواد" />
                  </SelectTrigger>
                  <SelectContent>
                    <SelectItem value="all">جميع المواد</SelectItem>
                    {subjects.map((subject) => (
                      <SelectItem key={subject} value={subject}>{subject}</SelectItem>
                    ))}
                  </SelectContent>
                </Select>
              </div>
              <div>
                <label className="block text-sm font-medium mb-2">الصف</label>
                <Select value={filters.grade} onValueChange={(value) => setFilters({ ...filters, grade: value })}>
                  <SelectTrigger className="border-yellow-200">
                    <SelectValue placeholder="جميع الصفوف" />
                  </SelectTrigger>
                  <SelectContent>
                    <SelectItem value="all">جميع الصفوف</SelectItem>
                    {grades.map((grade) => (
                      <SelectItem key={grade} value={grade}>{grade}</SelectItem>
                    ))}
                  </SelectContent>
                </Select>
              </div>
              <div className="flex items-end">
                <Button 
                  variant="outline" 
                  onClick={() => setFilters({ subject: 'all', grade: 'all', search: '' })}
                  className="w-full border-yellow-300 text-gray-900 hover:bg-yellow-100"
                >
                  مسح الفلاتر
                </Button>
              </div>
            </div>
          </CardContent>
        </Card>
      </div>

      {/* Quizzes Grid */}
      {loading ? (
        <div className="flex justify-center items-center h-64">
          <div className="animate-spin rounded-full h-12 w-12 border-b-2 border-yellow-500"></div>
        </div>
      ) : (
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
          {filteredQuizzes.map((quiz) => (
            <Card key={quiz.id} className="hover:shadow-lg transition-shadow border-yellow-200">
              <CardHeader>
                <div className="flex justify-between items-start">
                  <CardTitle className="text-lg text-gray-900">{quiz.title}</CardTitle>
                  <Badge variant={quiz.isActive ? "default" : "secondary"}>
                    {quiz.isActive ? "متاح" : "غير متاح"}
                  </Badge>
                </div>
              </CardHeader>
              <CardContent>
                <div className="space-y-3">
                  <p className="text-amber-800 text-sm">{quiz.description}</p>
                  
                  <div className="flex flex-wrap gap-2">
                    <Badge variant="outline" className="border-amber-300 text-amber-900">{quiz.subject}</Badge>
                    <Badge variant="outline" className="border-amber-300 text-amber-900">{quiz.grade}</Badge>
                  </div>

                  <div className="text-sm text-amber-800 space-y-1">
                    <div>عدد الأسئلة: {quiz.totalQuestions ?? quiz.questions?.length ?? 0}</div>
                    <div>المدة: {quiz.timeLimit} دقيقة</div>
                    {typeof quiz.passingScore === 'number' && (
                      <div>نسبة النجاح: {quiz.passingScore}%</div>
                    )}
                    {quiz.createdAt && (
                      <div>تاريخ الإنشاء: {new Date(quiz.createdAt).toLocaleString('en-US', { year: 'numeric', month: 'short', day: 'numeric' })}</div>
                    )}
                  </div>

                  <div className="pt-2">
                    <Button 
                      onClick={() => handleStartQuiz(quiz)}
                      disabled={!quiz.isActive}
                      className="w-full bg-amber-500 text-black hover:bg-amber-400"
                    >
                      {quiz.isActive ? 'ابدأ الكويز' : 'غير متاح'}
                    </Button>
                  </div>
                </div>
              </CardContent>
            </Card>
          ))}
        </div>
      )}

      {!loading && filteredQuizzes.length === 0 && (
        <div className="text-center py-12">
          <div className="text-gray-400 text-6xl mb-4">📚</div>
          <p className="text-gray-500 text-lg">لا توجد كويزات متاحة</p>
          <p className="text-gray-400 text-sm">جرب تغيير الفلاتر أو العودة لاحقاً</p>
        </div>
      )}
    </div>
  );
};

export default QuizzesPage; 