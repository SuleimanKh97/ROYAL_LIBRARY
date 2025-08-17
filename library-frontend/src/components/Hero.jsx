import { Button } from '@/components/ui/button';
import { BookOpen, Circle, Book, Calendar, FileText, Award, Lightbulb } from 'lucide-react';
import { Link } from 'react-router-dom';

const Hero = () => {
  const features = [
    {
      icon: Circle,
      title: 'كويزاتك التفاعلية',
      description: 'اختبر معلوماتك في جميع المواد',
      href: '/quizzes',
      color: 'bg-gradient-to-br from-blue-500 to-blue-600',
      hoverColor: 'hover:from-blue-600 hover:to-blue-700'
    },
    {
      icon: Book,
      title: 'سوق الطلاب',
      description: 'كتب ودوسيات وقرطاسية',
      href: '/books',
      color: 'bg-gradient-to-br from-green-500 to-green-600',
      hoverColor: 'hover:from-green-600 hover:to-green-700'
    },
    {
      icon: Award,
      title: 'دليل النجاح',
      description: 'ملخصات وأسئلة سنوات سابقة وأوراق عمل',
      href: '/success-guide',
      color: 'bg-gradient-to-br from-purple-500 to-purple-600',
      hoverColor: 'hover:from-purple-600 hover:to-purple-700'
    },
    {
      icon: Lightbulb,
      title: 'نصائح وإرشادات',
      description: 'جداول تنظيم الوقت ونصائح للتفوق',
      href: '/study-tips',
      color: 'bg-gradient-to-br from-yellow-500 to-yellow-600',
      hoverColor: 'hover:from-yellow-600 hover:to-yellow-700'
    },
    {
      icon: Calendar,
      title: 'الرزنامة الطلابية',
      description: 'جداول ومواعيد مهمة',
      href: '/calendar',
      color: 'bg-gradient-to-br from-orange-500 to-orange-600',
      hoverColor: 'hover:from-orange-600 hover:to-orange-700'
    }
  ];

  return (
    <section className="relative overflow-hidden">
      {/* Hero Background */}
      <div className="royal-gradient py-20 lg:py-32 relative">
        <div className="absolute inset-0 bg-gradient-to-br from-black/20 to-transparent"></div>
        <div className="container mx-auto px-4 text-center relative z-10">
          <div className="max-w-4xl mx-auto">
            {/* Logo */}
            <div className="mb-8 flex justify-center">
              <div className="w-24 h-24 lg:w-32 lg:h-32 bg-gradient-to-br from-royal-gold to-yellow-500 rounded-2xl flex items-center justify-center shadow-2xl hover:scale-110 transition-transform duration-500 overflow-hidden">
                <img 
                  src="/royal-study-logo.png" 
                  alt="ROYAL STUDY Logo" 
                  className="w-20 h-20 lg:w-28 lg:h-28 object-contain"
                  onError={(e) => {
                    e.target.style.display = 'none';
                    e.target.nextSibling.style.display = 'flex';
                  }}
                />
                <BookOpen className="w-16 h-16 lg:w-24 lg:h-24 text-royal-black hidden" />
              </div>
            </div>
            
            <h1 className="text-4xl lg:text-6xl font-black text-white mb-6 tracking-tight">
              مكتبة <span className="text-royal-gold font-black">Royal Study</span>
            </h1>
            <p className="text-xl lg:text-2xl text-royal-beige mb-8 font-medium leading-relaxed">
              وجهتك الأولى للتفوق الأكاديمي - دوسيات، كتب، قرطاسية وكورسات تعليمية
            </p>
            <p className="text-lg text-royal-beige/90 mb-10 max-w-3xl mx-auto leading-relaxed">
              متخصصون في خدمة طلاب التوجيهي الأردني بأفضل المواد التعليمية والدعم الأكاديمي
            </p>
            
            <div className="flex flex-col sm:flex-row gap-4 justify-center items-center">
              <Button 
                size="lg"
                className="bg-royal-gold hover:bg-royal-gold/90 text-royal-black font-bold px-8 py-4 text-lg rounded-xl shadow-xl hover:shadow-2xl transform hover:scale-105 transition-all duration-300"
                onClick={() => window.open('https://wa.me/962791234567?text=مرحباً، أرغب في الاستفسار عن خدماتكم', '_blank')}
              >
                ابدأ رحلتك التعليمية
              </Button>
              <Link to="/books">
                <Button 
                  size="lg"
                  variant="outline"
                  className="border-2 border-royal-gold text-royal-gold hover:bg-royal-gold hover:text-royal-black font-bold px-8 py-4 text-lg rounded-xl shadow-lg hover:shadow-xl transform hover:scale-105 transition-all duration-300"
                >
                  تصفح المنتجات
                </Button>
              </Link>
            </div>
          </div>
        </div>
      </div>

      {/* Features Grid */}
      <div className="bg-gradient-to-br from-royal-beige to-royal-dark-beige py-20">
        <div className="container mx-auto px-4">
          <div className="text-center mb-16">
            <h2 className="text-4xl lg:text-5xl font-black text-royal-black mb-6 tracking-tight">
              خدماتنا المميزة
            </h2>
            <p className="text-xl text-royal-black/80 max-w-3xl mx-auto leading-relaxed font-medium">
              نقدم لك مجموعة شاملة من الخدمات التعليمية لضمان تفوقك الأكاديمي
            </p>
          </div>

          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
            {features.map((feature, index) => {
              const Icon = feature.icon;
              return (
                <Link
                  key={index}
                  to={feature.href}
                  className="group relative"
                >
                  <div className="bg-white rounded-2xl p-8 shadow-lg hover:shadow-2xl transform hover:scale-105 transition-all duration-300 relative overflow-hidden">
                    <div className="absolute inset-0 bg-gradient-to-br from-transparent to-gray-50 opacity-0 group-hover:opacity-100 transition-opacity duration-300"></div>
                    <div className={`w-16 h-16 ${feature.color} ${feature.hoverColor} rounded-2xl flex items-center justify-center mb-6 group-hover:scale-110 transition-all duration-300 shadow-lg relative z-10`}>
                      <Icon className="w-8 h-8 text-white" />
                    </div>
                    <h3 className="text-xl font-bold text-royal-black mb-3 relative z-10">
                      {feature.title}
                    </h3>
                    <p className="text-royal-black/70 leading-relaxed relative z-10">
                      {feature.description}
                    </p>
                  </div>
                </Link>
              );
            })}
          </div>
        </div>
      </div>

      {/* Stats Section */}
      <div className="bg-white py-20 relative overflow-hidden">
        <div className="absolute inset-0 bg-gradient-to-r from-transparent via-royal-gold/5 to-transparent"></div>
        <div className="container mx-auto px-4 relative z-10">
          <div className="grid grid-cols-2 lg:grid-cols-4 gap-8 text-center">
            {[
              { number: '500+', label: 'طالب راضٍ', icon: '👥' },
              { number: '200+', label: 'دوسية ومنتج', icon: '📚' },
              { number: '50+', label: 'كويز تفاعلي', icon: '🧠' },
              { number: '5+', label: 'سنوات خبرة', icon: '⭐' }
            ].map((stat, index) => (
              <div key={index} className="group p-6 rounded-2xl hover:bg-royal-beige/30 transition-all duration-300">
                <div className="text-4xl mb-3">{stat.icon}</div>
                <div className="text-4xl lg:text-5xl font-black text-royal-gold mb-3 group-hover:scale-110 transition-transform duration-300">
                  {stat.number}
                </div>
                <div className="text-royal-black/80 font-semibold text-lg">
                  {stat.label}
                </div>
              </div>
            ))}
          </div>
        </div>
      </div>
    </section>
  );
};

export default Hero;
