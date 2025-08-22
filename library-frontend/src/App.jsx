import React, { useState, useEffect } from 'react'
import { Routes, Route, useNavigate, useLocation } from 'react-router-dom'
import { Button } from '@/components/ui/button.jsx'
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card.jsx'
import { Badge } from '@/components/ui/badge.jsx'
import { Input } from '@/components/ui/input.jsx'
import { Label } from '@/components/ui/label.jsx'
import { Dialog, DialogContent, DialogDescription, DialogHeader, DialogTitle, DialogTrigger } from '@/components/ui/dialog.jsx'
import { Search, Book, Users, Star, MessageCircle, Phone, Mail, MapPin, Heart, ShoppingCart, Settings, Edit, Trash2, Sparkles, Zap, Target, Award, Shield, Truck, Clock, CheckCircle } from 'lucide-react'
import AdminPanel from './components/AdminPanel.jsx'
import Header from './components/Header.jsx'
import Hero from './components/Hero.jsx'
import Footer from './components/Footer.jsx'
import BooksPage from './pages/BooksPage.jsx'
import AuthorsPage from './pages/AuthorsPage.jsx'
import CategoriesPage from './pages/CategoriesPage.jsx'
import ContactPage from './pages/ContactPage.jsx'
import RegisterPage from './pages/RegisterPage.jsx'
import LoginPage from './pages/LoginPage.jsx'
import QuizzesPage from './pages/QuizzesPage.jsx'
import QuizPage from './pages/QuizPage.jsx'
import QuizResultsPage from './pages/QuizResultsPage.jsx'
import MyAttemptsPage from './pages/MyAttemptsPage.jsx'
import CalendarPage from './pages/CalendarPage.jsx'
import AdminCalendarPage from './pages/AdminCalendarPage.jsx'
import SuccessGuidePage from './pages/SuccessGuidePage.jsx'
import StudyTipsPage from './pages/StudyTipsPage.jsx'
import { showSuccess, showError } from './lib/sweetAlert.js'

import apiService from './lib/api.js'
import './App.css'
import { fixImageUrl } from './lib/api.js'





// Features Section with Neumorphism - Black & Gold Theme
function FeaturesSection() {
  const features = [
    {
      icon: <Shield className="h-12 w-12" />,
      title: "ضمان الأصالة",
      description: "جميع الكتب أصلية 100% مع ضمان الجودة",
      color: "from-yellow-400 to-yellow-600"
    },
    {
      icon: <Truck className="h-12 w-12" />,
      title: "شحن سريع",
      description: "توصيل مجاني لجميع أنحاء إربد خلال 24 ساعة",
      color: "from-yellow-500 to-yellow-700"
    },
    {
      icon: <Clock className="h-12 w-12" />,
      title: "دعم 24/7",
      description: "فريق دعم متاح على مدار الساعة عبر واتساب",
      color: "from-yellow-600 to-yellow-800"
    },
    {
      icon: <Award className="h-12 w-12" />,
      title: "أسعار منافسة",
      description: "أفضل الأسعار مع عروض وخصومات مستمرة",
      color: "from-yellow-700 to-yellow-900"
    },
    {
      icon: <CheckCircle className="h-12 w-12" />,
      title: "سهولة الطلب",
      description: "طلب بسيط عبر واتساب مع دفع عند الاستلام",
      color: "from-yellow-800 to-yellow-950"
    },
    {
      icon: <Target className="h-12 w-12" />,
      title: "كتب مختارة",
      description: "مجموعة متنوعة من أفضل الكتب العربية",
      color: "from-yellow-900 to-black"
    }
  ]

  return (
    <section className="py-24 bg-gradient-to-br from-[#EBB026] via-[#F4C430] to-[#FFD700]" dir="rtl">
      <div className="container mx-auto px-4">
        <div className="text-center mb-16">
          <h2 className="text-5xl font-bold text-royal-black mb-6">✨ مميزاتنا الفريدة</h2>
          <p className="text-2xl text-royal-black/90 font-medium">🎯 نقدم لك تجربة شراء استثنائية</p>
        </div>
        
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
          {features.map((feature, index) => (
            <div key={index} className="bg-white/20 backdrop-blur-sm p-8 rounded-3xl hover:bg-white/30 transition-all duration-500 border border-royal-black/20">
              <div className={`w-16 h-16 bg-gradient-to-br from-royal-black to-gray-800 rounded-2xl flex items-center justify-center text-white mb-6 mx-auto`}>
                {feature.icon}
              </div>
              <h3 className="text-2xl font-bold text-royal-black mb-4 text-center">{feature.title}</h3>
              <p className="text-royal-black/80 text-center leading-relaxed">{feature.description}</p>
            </div>
          ))}
        </div>
      </div>
    </section>
  )
}

// Book Card Component with Glassmorphism
function BookCard({ book, onWhatsAppInquiry }) {
  const [isLoading, setIsLoading] = useState(false)

  const handleWhatsAppInquiry = async () => {
    setIsLoading(true)
    try {
      await onWhatsAppInquiry(book)
    } catch (error) {
      console.error('Error with WhatsApp inquiry:', error)
    } finally {
      setIsLoading(false)
    }
  }

  const getBookImage = () => {
    if (book.coverImageUrl) {
      return fixImageUrl(book.coverImageUrl)
    }
    if (book.images && book.images.length > 0) {
      return fixImageUrl(book.images[0].imageUrl)
    }
    return 'https://via.placeholder.com/300x400/f0f0f0/666?text=كتاب'
  }

  const handleImageError = (event) => {
    console.log('Image failed to load:', event.target.src);
    // Set fallback image
    event.target.src = 'data:image/svg+xml;base64,PHN2ZyB3aWR0aD0iMzAwIiBoZWlnaHQ9IjQwMCIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIj4KICA8cmVjdCB3aWR0aD0iMzAwIiBoZWlnaHQ9IjQwMCIgZmlsbD0iI2YwZjBmMCIvPgogIDx0ZXh0IHg9IjE1MCIgeT0iMjAwIiBmb250LWZhbWlseT0iQXJpYWwiIGZvbnQtc2l6ZT0iMjAiIGZpbGw9IiM2NjY2NjYiIHRleHQtYW5jaG9yPSJtaWRkbGUiIGR5PSIuM2VtIj7Zhtin2YbYqDwvdGV4dD4KPC9zdmc+';
  }

  const getBookTitle = () => {
    return book.titleArabic || book.title || 'عنوان غير محدد'
  }

  const getAuthorName = () => {
    if (book.author) {
      return book.author.nameArabic || book.author.name
    }
    return 'مؤلف غير محدد'
  }

  const getCategoryName = () => {
    if (book.category) {
      return book.category.nameArabic || book.category.name
    }
    return 'تصنيف غير محدد'
  }

  const getPrice = () => {
    return book.price ? `${book.price} د.أ` : 'السعر غير محدد'
  }

  const isAvailable = () => {
    return book.isAvailable && book.stockQuantity > 0
  }

  return (
    <Card className="glass-card overflow-hidden hover-lift transition-all duration-500" dir="rtl">
      <div className="relative">
        {/* Book Image */}
        <div className="aspect-[3/4] bg-gradient-to-br from-amber-100 to-amber-200 flex items-center justify-center">
          <img
            src={getBookImage()}
            alt={getBookTitle()}
            className="w-full h-full object-cover"
            onError={handleImageError}
          />
        </div>

        {/* Badges with Neumorphism */}
        <div className="absolute top-3 right-3 flex flex-col gap-2">
          {book.isFeatured && (
            <div className="neumorphism px-3 py-1 rounded-xl">
              <Badge className="bg-gradient-to-r from-yellow-400 to-yellow-600 text-white border-0">
                ⭐ مميز
              </Badge>
            </div>
          )}
          {book.isNewRelease && (
            <div className="neumorphism px-3 py-1 rounded-xl">
              <Badge className="bg-gradient-to-r from-green-400 to-green-600 text-white border-0">
                🆕 جديد
              </Badge>
            </div>
          )}
        </div>
      </div>

      <CardContent className="p-6">
        <div className="space-y-4">
          <h3 className="font-bold text-xl text-right line-clamp-2 text-gray-900">
            {getBookTitle()}
          </h3>
          
          <div className="space-y-2">
            <p className="text-sm text-gray-700 text-right">
              <span className="font-semibold">المؤلف:</span> {getAuthorName()}
            </p>
            
            <p className="text-sm text-gray-600 text-right">
              <span className="font-semibold">التصنيف:</span> {getCategoryName()}
            </p>
          </div>
          
          <div className="flex items-center justify-between pt-4">
            <div className="text-right">
              <p className="font-bold text-2xl text-gradient-to-r from-yellow-600 to-yellow-800">{getPrice()}</p>
              <p className={`text-sm font-semibold ${isAvailable() ? 'text-green-600' : 'text-red-600'}`}>
                {isAvailable() ? '✅ متوفر' : '❌ غير متوفر'}
              </p>
            </div>
          </div>
          
          <div className="pt-4">
            <Button
              size="lg"
              onClick={handleWhatsAppInquiry}
              disabled={isLoading}
              className="w-full neumorphism text-black hover:bg-yellow-500 hover:text-black transition-all duration-300 rounded-xl py-3"
            >
              <MessageCircle className="h-5 w-5 ml-2" />
              {isLoading ? 'جاري الإرسال...' : '💬 استفسار واتساب'}
            </Button>
          </div>
        </div>
      </CardContent>
    </Card>
  )
}

// Books Grid Component
function BooksGrid({ books, loading, onWhatsAppInquiry }) {
  if (loading) {
    return (
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-8">
        {[...Array(8)].map((_, index) => (
          <Card key={index} className="glass-card overflow-hidden">
            <div className="aspect-[3/4] bg-gradient-to-br from-amber-100 to-amber-200 animate-pulse shimmer"></div>
            <CardContent className="p-6">
              <div className="space-y-4">
                <div className="h-6 bg-gradient-to-r from-amber-200 to-amber-300 rounded animate-pulse shimmer"></div>
                <div className="h-4 bg-gradient-to-r from-amber-200 to-amber-300 rounded animate-pulse shimmer w-3/4"></div>
                <div className="h-4 bg-gradient-to-r from-amber-200 to-amber-300 rounded animate-pulse shimmer w-1/2"></div>
              </div>
            </CardContent>
          </Card>
        ))}
      </div>
    )
  }

  if (!books || books.length === 0) {
    return (
      <div className="text-center py-16">
        <div className="neumorphism p-12 rounded-3xl inline-block">
          <Book className="h-20 w-20 text-amber-400 mx-auto mb-6" />
          <h3 className="text-2xl font-bold text-amber-800 mb-4">لا توجد كتب</h3>
          <p className="text-amber-600">لم يتم العثور على كتب في هذا القسم</p>
        </div>
      </div>
    )
  }

  return (
    <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-8">
      {books.map((book) => (
        <BookCard
          key={book.id}
          book={book}
          onWhatsAppInquiry={onWhatsAppInquiry}
        />
      ))}
    </div>
  )
}







// Main App Component
function App() {
  const navigate = useNavigate()
  const location = useLocation()
  const [books, setBooks] = useState([])
  const [featuredBooks, setFeaturedBooks] = useState([])
  const [newReleases, setNewReleases] = useState([])
  const [categories, setCategories] = useState([])
  const [loading, setLoading] = useState(true)
  const [searchTerm, setSearchTerm] = useState('')
  const [currentUser, setCurrentUser] = useState(null)
  const [showAdminPanel, setShowAdminPanel] = useState(false)

  const [selectedCategoryForBooks, setSelectedCategoryForBooks] = useState('all')
  const [selectedAuthorForBooks, setSelectedAuthorForBooks] = useState('all')

  useEffect(() => {
    loadInitialData()
    // Load user from localStorage on app start
    const savedUser = localStorage.getItem('user')
    if (savedUser) {
      try {
        setCurrentUser(JSON.parse(savedUser))
      } catch (error) {
        console.error('Error parsing saved user:', error)
        localStorage.removeItem('user')
      }
    } else {
    setCurrentUser(apiService.getCurrentUser())
    }
  }, [])

  // Get current page from location
  const getCurrentPage = () => {
    const pathname = location.pathname
    
    if (pathname.startsWith('/quiz/') && pathname !== '/quiz/') {
      return 'quiz'
    } else if (pathname.startsWith('/quiz-results')) {
      return 'quiz-results'
    } else if (pathname.startsWith('/my-attempts')) {
      return 'my-attempts'
    } else if (pathname.startsWith('/quizzes')) {
      return 'quizzes'
    } else if (pathname.startsWith('/books')) {
      return 'books'
    } else if (pathname.startsWith('/authors')) {
      return 'authors'
    } else if (pathname.startsWith('/categories')) {
      return 'categories'
    } else if (pathname.startsWith('/contact')) {
      return 'contact'
    } else if (pathname.startsWith('/register')) {
      return 'register'
    } else if (pathname === '/' || pathname === '') {
      return 'home'
    }
    return 'home'
  }

  const loadInitialData = async () => {
    setLoading(true)
    try {
      const [booksResponse, categoriesResponse] = await Promise.all([
        apiService.getBooks(1, 50),
        apiService.getCategories()
      ])

      if (booksResponse && booksResponse.items) {
        setBooks(booksResponse.items)
        setFeaturedBooks(booksResponse.items.filter(book => book.isFeatured))
        setNewReleases(booksResponse.items.filter(book => book.isNewRelease))
      } else {
        setBooks([])
        setFeaturedBooks([])
        setNewReleases([])
      }
      
      if (categoriesResponse) {
        setCategories(categoriesResponse)
      } else {
        setCategories([])
      }
    } catch (error) {
      console.error('Error loading initial data:', error)
      setBooks([])
      setFeaturedBooks([])
      setNewReleases([])
      setCategories([])
    } finally {
      setLoading(false)
    }
  }

  const handleSearch = async (term) => {
    setSearchTerm(term)
    setLoading(true)
    
    try {
      if (term.trim()) {
        const response = await apiService.getBooks(1, 50, term)
        if (response.books) {
          setBooks(response.books)
        } else {
          setBooks([])
        }
      } else {
        // Reload all books when search is cleared
        const response = await apiService.getBooks(1, 50)
        if (response.books) {
          setBooks(response.books)
        }
      }
    } catch (error) {
      console.error('Error searching books:', error)
      setBooks([])
    } finally {
      setLoading(false)
    }
  }

  const handleLogin = (user, token = null) => {
    setCurrentUser(user)
    
    // If token is provided (from registration), store it
    if (token) {
      localStorage.setItem('token', token)
      localStorage.setItem('user', JSON.stringify(user))
    }
  }

  const handleLogout = async () => {
    try {
      await apiService.logout()
    } catch (error) {
      console.error('Logout error:', error)
    } finally {
      setCurrentUser(null)
      setShowAdminPanel(false)
      // Clear localStorage
      localStorage.removeItem('token')
      localStorage.removeItem('user')
    }
  }

  const handleWhatsAppInquiry = async (book) => {
    try {
      // For demo purposes, use mock customer data
      const customerData = {
        customerName: currentUser ? currentUser.firstName + ' ' + (currentUser.lastName || '') : 'زائر',
        customerPhone: '+962785462983', // Mock phone number
        customerEmail: currentUser?.email || '',
        bookId: book.id,
        message: `أريد الاستفسار عن كتاب "${book.titleArabic || book.title}"`
      }

      // Try to create inquiry via API
      try {
        const inquiry = await apiService.createBookInquiry(customerData)
        console.log('Inquiry created:', inquiry)
      } catch (error) {
        console.log('API not available, using mock WhatsApp URL')
      }

      // Generate WhatsApp URL
      const libraryPhone = '+962785462983' // Library WhatsApp number
      const message = `السلام عليكم ورحمة الله وبركاته

أريد الاستفسار عن الكتاب التالي:

*${book.titleArabic || book.title}*
المؤلف: ${book.authorNameArabic || book.authorName || book.author?.nameArabic || book.author?.name}
التصنيف: ${book.categoryNameArabic || book.categoryName || book.category?.nameArabic || book.category?.name}
السعر: ${book.price} د.أ
المخزون: ${book.stockQuantity > 0 ? 'متوفر' : 'غير متوفر'}

${customerData.message}

شكراً لكم
${customerData.customerName}`

      const whatsappUrl = `https://wa.me/${libraryPhone.replace('+', '')}?text=${encodeURIComponent(message)}`
      
      // Check if it's iOS device
      const isIOS = /iPad|iPhone|iPod/.test(navigator.userAgent);
      
      if (isIOS) {
        // For iOS, try to open WhatsApp app directly
        const whatsappAppUrl = `whatsapp://send?phone=${libraryPhone.replace('+', '')}&text=${encodeURIComponent(message)}`;
        
        // Try to open WhatsApp app first
        window.location.href = whatsappAppUrl;
        
        // Fallback to web version after a short delay
        setTimeout(() => {
          window.open(whatsappUrl, '_blank');
        }, 2000);
      } else {
        // For other devices, open in new tab
        window.open(whatsappUrl, '_blank');
      }
      
      // Show success message
      showSuccess('تم فتح الواتساب! يمكنك الآن إرسال استفسارك.')
      
    } catch (error) {
      console.error('Error creating WhatsApp inquiry:', error)
      showError('حدث خطأ أثناء إنشاء الاستفسار. يرجى المحاولة مرة أخرى.')
    }
  }

  const handleOpenAdmin = () => {
    setShowAdminPanel(true)
  }



  // Handle page navigation
  const handleBackToHome = () => {
    navigate('/')
    setSelectedCategoryForBooks('all')
    setSelectedAuthorForBooks('all')
    // Scroll to top when returning home
    window.scrollTo({ top: 0, behavior: 'smooth' })
  }

  const handleViewAuthor = (author) => {
    // TODO: Implement author details modal
    console.log('View author:', author)
  }

  const handleViewAuthorBooks = (author) => {
    navigate('/books')
    setSelectedAuthorForBooks(author.id.toString())
  }

  const handleViewCategory = (category) => {
    navigate('/books')
    setSelectedCategoryForBooks(category.id.toString())
  }

  // Handle search from any page
  const handleGlobalSearch = (term) => {
    if (getCurrentPage() !== 'home') {
      navigate('/')
      setSelectedCategoryForBooks('all')
      setSelectedAuthorForBooks('all')
      // Wait for page change then search
      setTimeout(() => {
        handleSearch(term)
      }, 100)
    } else {
      handleSearch(term)
    }
  }

  // Scroll to section helper
  const scrollToSection = (sectionId) => {
    if (getCurrentPage() !== 'home') {
      navigate('/')
      setSelectedCategoryForBooks('all')
      setSelectedAuthorForBooks('all')
      // Wait for page change then scroll
      setTimeout(() => {
        const element = document.getElementById(sectionId)
        if (element) {
          element.scrollIntoView({ behavior: 'smooth' })
        }
      }, 100)
    } else {
      const element = document.getElementById(sectionId)
      if (element) {
        element.scrollIntoView({ behavior: 'smooth' })
      }
    }
  }

  // Render different pages
  if (showAdminPanel) {
    return <AdminPanel currentUser={currentUser} onClose={() => setShowAdminPanel(false)} />
  }

  return (
    <div className="min-h-screen">
      <Header 
          currentUser={currentUser}
          onLogout={handleLogout}
          onOpenAdmin={() => setShowAdminPanel(true)}
      />
      
      <Routes>
        <Route path="/" element={
          <>
            <main>
              <Hero />
              
              {/* Features Section */}
              <FeaturesSection />
              





            </main>
            <Footer />
          </>
        } />
        
        <Route path="/books" element={
          <>
            <BooksPage 
              onBack={handleBackToHome}
              onWhatsAppInquiry={handleWhatsAppInquiry}
              initialCategory={selectedCategoryForBooks}
              initialAuthor={selectedAuthorForBooks}
            />
            <Footer />
          </>
        } />
        
        <Route path="/authors" element={
          <>
            <AuthorsPage 
              onBack={handleBackToHome}
              onViewAuthor={handleViewAuthor}
              onViewAuthorBooks={handleViewAuthorBooks}
            />
            <Footer />
          </>
        } />
        
        <Route path="/categories" element={
          <>
            <CategoriesPage 
              onBack={handleBackToHome}
              onViewCategory={handleViewCategory}
            />
            <Footer />
          </>
        } />
        
        <Route path="/contact" element={
          <>
            <ContactPage 
              onBack={handleBackToHome}
            />
            <Footer />
          </>
        } />
        
        <Route path="/register" element={
          <>
            <RegisterPage 
              onBack={handleBackToHome}
              onLoginSuccess={handleLogin}
            />
            <Footer />
          </>
        } />
        
        <Route path="/login" element={
          <>
            <LoginPage 
              onLoginSuccess={handleLogin}
            />
            <Footer />
          </>
        } />
        
        <Route path="/quizzes" element={
          <>
            <QuizzesPage />
            <Footer />
          </>
        } />
        
        <Route path="/quiz/:quizId" element={
          <>
            <QuizPage />
            <Footer />
          </>
        } />
        
        <Route path="/quiz-results" element={
          <>
            <QuizResultsPage />
            <Footer />
          </>
        } />
        
        <Route path="/my-attempts" element={
          <>
            <MyAttemptsPage />
            <Footer />
          </>
        } />
        
        <Route path="/calendar" element={
          <>
            <CalendarPage />
            <Footer />
          </>
        } />
        
        <Route path="/admin/calendar" element={
          <>
            <AdminCalendarPage />
            <Footer />
          </>
        } />
        
        <Route path="/success-guide" element={
          <>
            <SuccessGuidePage />
            <Footer />
          </>
        } />
        
        <Route path="/study-tips" element={
          <>
            <StudyTipsPage />
            <Footer />
          </>
        } />
      </Routes>



    </div>
  )
}

export default App

