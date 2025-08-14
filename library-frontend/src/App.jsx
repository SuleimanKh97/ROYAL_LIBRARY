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
import BooksPage from './pages/BooksPage.jsx'
import AuthorsPage from './pages/AuthorsPage.jsx'
import CategoriesPage from './pages/CategoriesPage.jsx'
import ContactPage from './pages/ContactPage.jsx'
import RegisterPage from './pages/RegisterPage.jsx'
import QuizzesPage from './pages/QuizzesPage.jsx'
import QuizPage from './pages/QuizPage.jsx'
import QuizResultsPage from './pages/QuizResultsPage.jsx'
import MyAttemptsPage from './pages/MyAttemptsPage.jsx'
import { showSuccess, showError } from './lib/sweetAlert.js'

import apiService from './lib/api.js'
import './App.css'

// Header Component with Glassmorphism
function Header({ onSearch, currentUser, onLogin, onLogout, onOpenAdmin, scrollToSection }) {
  const [searchTerm, setSearchTerm] = useState('')
  const [isMenuOpen, setIsMenuOpen] = useState(false)
  const [isScrolled, setIsScrolled] = useState(false)

  useEffect(() => {
    const handleScroll = () => {
      setIsScrolled(window.scrollY > 10)
    }
    window.addEventListener('scroll', handleScroll)
    return () => window.removeEventListener('scroll', handleScroll)
  }, [])

  const handleSearch = (e) => {
    e.preventDefault()
    onSearch(searchTerm)
  }

  const navigate = useNavigate()
  
  const handleNavigation = (page) => {
    navigate(`/${page}`)
    setIsMenuOpen(false)
  }

  return (
    <header className={`sticky top-0 z-50 transition-all duration-500 ${isScrolled ? 'bg-black/90 backdrop-blur-xl border-b border-yellow-400/30' : 'bg-black/80 backdrop-blur-xl border-b border-yellow-400/20'}`} dir="rtl">
      <div className="container mx-auto px-4 py-4">
        <div className="flex items-center justify-between">
          {/* Logo with Neumorphism - Black & Gold Theme */}
          <div className="flex items-center gap-4 cursor-pointer hover:opacity-90 transition-all duration-300" onClick={() => navigate('/')}>
            <div className="neumorphism p-3 rounded-2xl hover-lift">
              <img 
                src="/royal-study-logo.png" 
                alt="ROYAL STUDY Logo" 
                className="h-12 w-12 md:h-14 md:w-14 object-contain"
                onError={(e) => {
                  e.target.style.display = 'none';
                  e.target.nextSibling.style.display = 'block';
                }}
              />
              <Book className="h-12 w-12 md:h-14 md:w-14 text-gradient-to-r from-yellow-500 to-yellow-600 hidden" />
            </div>
            <div className="hidden md:block">
              <h1 className="text-2xl font-bold gradient-text">👑 ROYAL STUDY</h1>
              <p className="text-sm text-yellow-200">مكتبة إربد الأولى</p>
            </div>
          </div>

          {/* Desktop Navigation with Glassmorphism - Black & Gold Theme */}
          <nav className="hidden lg:flex items-center gap-2">
            <button 
              onClick={() => {
                scrollToSection('featured-books')
                setIsMenuOpen(false)
              }}
              className={`px-4 py-2 rounded-xl text-sm transition-all duration-300 hover-lift ${isScrolled ? 'bg-yellow-500/20 text-yellow-200 hover:bg-yellow-500/40 border border-yellow-400/30' : 'glass-card text-yellow-100 hover:bg-yellow-800/40'}`}
            >
              ⭐ الكتب المميزة
            </button>
            <button 
              onClick={() => handleNavigation('books')}
              className={`px-4 py-2 rounded-xl text-sm transition-all duration-300 hover-lift ${isScrolled ? 'bg-yellow-500/20 text-yellow-200 hover:bg-yellow-500/40 border border-yellow-400/30' : 'glass-card text-yellow-100 hover:bg-yellow-800/40'}`}
            >
              📚 جميع الكتب
            </button>
            <button 
              onClick={() => handleNavigation('quizzes')}
              className={`px-4 py-2 rounded-xl text-sm transition-all duration-300 hover-lift ${isScrolled ? 'bg-yellow-500/20 text-yellow-200 hover:bg-yellow-500/40 border border-yellow-400/30' : 'glass-card text-yellow-100 hover:bg-yellow-800/40'}`}
            >
              🧠 الكويزات
            </button>
            <button 
              onClick={() => handleNavigation('contact')}
              className={`px-4 py-2 rounded-xl text-sm transition-all duration-300 hover-lift ${isScrolled ? 'bg-yellow-500/20 text-yellow-200 hover:bg-yellow-500/40 border border-yellow-400/30' : 'glass-card text-yellow-100 hover:bg-yellow-800/40'}`}
            >
              📞 اتصل بنا
            </button>
          </nav>

          {/* Search Bar with Glassmorphism - Black & Gold Theme */}
          <form onSubmit={handleSearch} className="flex-1 max-w-md mx-4">
            <div className="relative">
              <Search className="absolute right-4 top-1/2 -translate-y-1/2 text-yellow-400 h-5 w-5" />
              <Input
                type="text"
                placeholder="🔍 ابحث عن كتابك المفضل..."
                value={searchTerm}
                onChange={(e) => setSearchTerm(e.target.value)}
                className={`pr-12 text-right border transition-all duration-300 rounded-2xl font-medium ${isScrolled ? 'bg-yellow-500/20 border-yellow-400/50 text-yellow-200 placeholder:text-yellow-300 focus:bg-yellow-500/30 focus:border-yellow-400' : 'glass-card bg-white/10 border-yellow-400/30 text-yellow-50 placeholder:text-yellow-200/70 focus:bg-white/20 focus:border-yellow-400/60'}`}
              />
            </div>
          </form>

          {/* User Actions with Neumorphism - Black & Gold Theme */}
          <div className="flex items-center gap-3">
            {currentUser ? (
              <div className="flex items-center gap-3">
                <div className={`hidden sm:flex items-center gap-3 px-4 py-2 rounded-2xl ${isScrolled ? 'bg-yellow-500/20 border border-yellow-400/30' : 'glass-card'}`}>
                  <div className="w-8 h-8 bg-gradient-to-r from-yellow-400 to-yellow-600 rounded-full flex items-center justify-center">
                    <span className="text-black text-sm font-bold">👤</span>
                  </div>
                  <span className={`text-sm ${isScrolled ? 'text-yellow-200' : 'text-yellow-100'}`}>مرحباً، {currentUser.firstName} 👋</span>
                </div>
                <Button 
                  variant="default" 
                  size="sm" 
                  onClick={() => navigate('/my-attempts')}
                  className={`hidden sm:flex items-center px-4 py-2 rounded-xl transition-all duration-300 hover-lift ${isScrolled ? 'bg-yellow-500 text-black hover:bg-yellow-400 border border-yellow-400' : 'neumorphism text-black hover:bg-yellow-400'}`}
                >
                  <span>📊 محاولاتي</span>
                </Button>
                {apiService.isAdmin() && (
                  <Button 
                    variant="default" 
                    size="sm" 
                    onClick={onOpenAdmin} 
                    className={`hidden sm:flex items-center px-4 py-2 rounded-xl transition-all duration-300 hover-lift ${isScrolled ? 'bg-yellow-600 text-black hover:bg-yellow-500 border border-yellow-400' : 'neumorphism text-black hover:bg-yellow-500'}`}
                  >
                    <Settings className="h-4 w-4 ml-2" />
                    <span>⚙️ لوحة التحكم</span>
                  </Button>
                )}
                <Button 
                  variant="default" 
                  size="sm" 
                  onClick={onLogout} 
                  className={`hidden sm:flex items-center px-4 py-2 rounded-xl transition-all duration-300 hover-lift ${isScrolled ? 'bg-red-500 text-white hover:bg-red-600 border border-red-400' : 'bg-gradient-to-r from-red-500 to-red-600 text-white hover:from-red-600 hover:to-red-700'}`}
                >
                  <span>🚪 تسجيل الخروج</span>
                </Button>
              </div>
            ) : (
              <div className="hidden sm:flex items-center gap-3">
                <Button 
                  onClick={onLogin} 
                  className={`border px-4 py-2 rounded-xl transition-all duration-300 hover-lift ${isScrolled ? 'bg-yellow-500/20 border-yellow-400/50 text-yellow-200 hover:bg-yellow-500/40' : 'glass-card border-yellow-400/30 text-yellow-100 hover:bg-yellow-800/40'}`}
                >
                  <span>🔑 تسجيل الدخول</span>
                </Button>
                <Button 
                  onClick={() => navigate('/register')} 
                  className={`px-4 py-2 rounded-xl transition-all duration-300 hover-lift ${isScrolled ? 'bg-yellow-500 text-black hover:bg-yellow-400 border border-yellow-400' : 'neumorphism text-black hover:bg-yellow-400'}`}
                >
                  <span>📝 التسجيل</span>
                </Button>
              </div>
            )}
            
            {/* Mobile Menu Button */}
            <button
              onClick={() => setIsMenuOpen(!isMenuOpen)}
              className={`lg:hidden p-3 rounded-xl transition-all duration-300 hover-lift ${isScrolled ? 'bg-yellow-500 text-black hover:bg-yellow-400 border border-yellow-400' : 'neumorphism text-black hover:bg-yellow-600'}`}
            >
              <svg className="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor" strokeWidth={3}>
                {isMenuOpen ? (
                  <path strokeLinecap="round" strokeLinejoin="round" d="M6 18L18 6M6 6l12 12" />
                ) : (
                  <path strokeLinecap="round" strokeLinejoin="round" d="M4 6h16M4 12h16M4 18h16" />
                )}
              </svg>
            </button>
          </div>
        </div>

        {/* Mobile Navigation Menu with Glassmorphism */}
        {isMenuOpen && (
          <nav className={`lg:hidden mt-4 pb-4 rounded-2xl ${isScrolled ? 'bg-yellow-500/20 border border-yellow-400/30' : 'glass-card'}`}>
            <div className="flex flex-col space-y-3 p-4">
              <button 
                onClick={() => {
                  scrollToSection('featured-books')
                  setIsMenuOpen(false)
                }}
                className={`text-right transition-all duration-300 py-3 px-4 rounded-xl hover-lift ${isScrolled ? 'text-yellow-200 hover:bg-yellow-500/40' : 'text-yellow-100 hover:bg-yellow-800/40'}`}
              >
                ⭐ الكتب المميزة
              </button>
              <button 
                onClick={() => handleNavigation('books')}
                className={`text-right transition-all duration-300 py-3 px-4 rounded-xl hover-lift ${isScrolled ? 'text-yellow-200 hover:bg-yellow-500/40' : 'text-yellow-100 hover:bg-yellow-800/40'}`}
              >
                📚 جميع الكتب
              </button>
              <button 
                onClick={() => handleNavigation('quizzes')}
                className={`text-right transition-all duration-300 py-3 px-4 rounded-xl hover-lift ${isScrolled ? 'text-yellow-200 hover:bg-yellow-500/40' : 'text-yellow-100 hover:bg-yellow-800/40'}`}
              >
                🧠 الكويزات
              </button>
              <button 
                onClick={() => handleNavigation('contact')}
                className={`text-right transition-all duration-300 font-bold py-3 px-6 rounded-xl hover-lift ${isScrolled ? 'text-black hover:text-white hover:bg-yellow-500' : 'text-black hover:text-white hover:bg-gradient-to-r hover:from-yellow-500 hover:to-yellow-600'}`}
              >
                📞 اتصل بنا
              </button>
              
              {/* Mobile User Actions */}
              {currentUser ? (
                <div className={`border-t pt-4 mt-4 ${isScrolled ? 'border-yellow-400/30' : 'border-amber-400/30'}`}>
                  <div className={`flex items-center gap-3 px-4 py-3 rounded-xl mb-3 ${isScrolled ? 'bg-yellow-500/20 border border-yellow-400/30' : 'glass-card'}`}>
                    <div className="w-8 h-8 bg-gradient-to-r from-yellow-400 to-yellow-600 rounded-full flex items-center justify-center">
                      <span className="text-black text-sm font-bold">👤</span>
                    </div>
                    <span className={`text-sm ${isScrolled ? 'text-yellow-200' : 'text-amber-100'}`}>مرحباً، {currentUser.firstName} 👋</span>
                  </div>
                                      <button 
                      onClick={() => {
                        navigate('/my-attempts')
                        setIsMenuOpen(false)
                      }}
                    className={`w-full text-right transition-all duration-300 py-3 px-4 rounded-xl mb-3 hover-lift ${isScrolled ? 'text-yellow-200 hover:bg-yellow-500/40' : 'text-amber-100 hover:bg-amber-800/40'}`}
                  >
                    📊 محاولاتي
                  </button>
                  {apiService.isAdmin() && (
                    <button 
                      onClick={onOpenAdmin}
                      className={`w-full text-right transition-all duration-300 py-3 px-4 rounded-xl mb-3 hover-lift ${isScrolled ? 'text-yellow-200 hover:bg-yellow-500/40' : 'text-amber-100 hover:bg-amber-800/40'}`}
                    >
                      ⚙️ لوحة التحكم
                    </button>
                  )}
                  <button 
                    onClick={onLogout}
                    className={`w-full text-right transition-all duration-300 py-3 px-4 rounded-xl hover-lift ${isScrolled ? 'text-yellow-200 hover:bg-yellow-500/40' : 'text-amber-100 hover:bg-amber-800/40'}`}
                  >
                    🚪 تسجيل الخروج
                  </button>
                </div>
              ) : (
                <div className={`border-t pt-4 mt-4 ${isScrolled ? 'border-yellow-400/30' : 'border-amber-400/30'}`}>
                  <button 
                    onClick={onLogin}
                    className={`w-full text-right transition-all duration-300 py-3 px-4 rounded-xl mb-3 hover-lift ${isScrolled ? 'text-yellow-200 hover:bg-yellow-500/40' : 'text-amber-100 hover:bg-amber-800/40'}`}
                  >
                    🔑 تسجيل الدخول
                  </button>
                  <button 
                    onClick={() => {
                      navigate('/register')
                      setIsMenuOpen(false)
                    }}
                    className={`w-full text-right transition-all duration-300 py-3 px-4 rounded-xl hover-lift ${isScrolled ? 'text-yellow-200 hover:bg-yellow-500/40' : 'text-amber-100 hover:bg-amber-800/40'}`}
                  >
                    📝 التسجيل
                  </button>
                </div>
              )}
            </div>
          </nav>
        )}
      </div>
    </header>
  )
}

// Hero Section with Aurora UI
function HeroSection({ scrollToSection }) {
  return (
    <section className="aurora-bg text-white py-32 relative overflow-hidden particles" dir="rtl">
      {/* Aurora Background Elements */}
      <div className="absolute inset-0 opacity-20">
        <div className="absolute top-20 left-20 text-8xl float">📚</div>
        <div className="absolute top-40 right-40 text-6xl float" style={{animationDelay: '2s'}}>✨</div>
        <div className="absolute bottom-40 left-40 text-7xl float" style={{animationDelay: '4s'}}>🌟</div>
        <div className="absolute bottom-20 right-20 text-5xl float" style={{animationDelay: '6s'}}>📖</div>
        <div className="absolute top-1/2 left-1/4 text-6xl float" style={{animationDelay: '1s'}}>🎯</div>
        <div className="absolute top-1/3 right-1/4 text-5xl float" style={{animationDelay: '3s'}}>💎</div>
      </div>
      
      <div className="container mx-auto px-4 text-center relative z-10">
        <div className="mb-16">
          {/* Main Title with Gradient Text */}
          <h1 className="text-6xl md:text-8xl font-extrabold mb-8 gradient-text drop-shadow-2xl">
            اقْرأ أكثر. تطوّر أسرع.
          </h1>
          
          {/* Subtitle with Glassmorphism - Black & Gold Theme */}
          <div className="glass-card inline-block px-8 py-4 rounded-2xl mb-6">
            <p className="text-3xl md:text-4xl text-yellow-100 font-bold drop-shadow-lg">
              آلاف الكتب العربية المختارة للأطفال واليافعين
            </p>
          </div>
          
          {/* Features with Neumorphism - Black & Gold Theme */}
          <div className="flex flex-wrap justify-center gap-4 mb-8">
            <div className="neumorphism-dark px-6 py-3 rounded-2xl">
              <p className="text-lg md:text-xl text-yellow-200 font-semibold">
                🚚 شحن سريع
              </p>
            </div>
            <div className="neumorphism-dark px-6 py-3 rounded-2xl">
              <p className="text-lg md:text-xl text-yellow-200 font-semibold">
                💯 ضمان الأصالة
              </p>
            </div>
            <div className="neumorphism-dark px-6 py-3 rounded-2xl">
              <p className="text-lg md:text-xl text-yellow-200 font-semibold">
                💸 أسعار منافسة
              </p>
            </div>
            <div className="neumorphism-dark px-6 py-3 rounded-2xl">
              <p className="text-lg md:text-xl text-yellow-200 font-semibold">
                🤝 دعم فوري
              </p>
            </div>
          </div>
        </div>
        
        {/* CTA Buttons with Hover Effects - Black & Gold Theme */}
        <div className="flex flex-wrap justify-center gap-6 md:gap-8">
          <Button 
            size="lg" 
            className="text-2xl px-10 md:px-12 py-6 neumorphism text-black hover:bg-yellow-400 font-extrabold rounded-3xl shadow-2xl transition-all duration-500 hover-lift hover-glow"
            onClick={() => navigate('/books')}
          >
            <Sparkles className="h-6 w-6 ml-3" />
            📚 تصفح الكتب الآن
          </Button>
          <Button 
            size="lg" 
            variant="outline" 
            className="text-2xl px-10 md:px-12 py-6 glass-card text-yellow-100 border border-yellow-300 hover:bg-yellow-300/40 hover:text-yellow-900 font-extrabold rounded-3xl shadow-2xl transition-all duration-500 hover-lift"
            onClick={() => scrollToSection && scrollToSection('new-releases')}
          >
            <Zap className="h-6 w-6 ml-3" />
            🆕 اكتشف الإصدارات الجديدة
          </Button>
          <Button 
            size="lg"
            variant="outline"
            className="text-2xl px-10 md:px-12 py-6 glass-card text-white hover:bg-white/20 border-2 border-yellow-400 font-extrabold rounded-3xl shadow-2xl transition-all duration-500 hover-lift hover-glow"
            onClick={() => window.open('https://wa.me/962785462983?text=%D8%A3%D8%B1%D9%8A%D8%AF%20%D9%85%D8%B3%D8%A7%D8%B9%D8%AF%D8%A9%20%D9%81%D9%8A%20%D8%A7%D8%AE%D8%AA%D9%8A%D8%A7%D8%B1%20%D9%83%D8%AA%D8%A8%20%D9%85%D9%86%20ROYAL%20STUDY', '_blank')}
          >
            <MessageCircle className="h-6 w-6 ml-3" />
            💬 تواصل عبر واتساب
          </Button>
        </div>

        {/* Stats Section with Glassmorphism - Black & Gold Theme */}
        <div className="mt-20 grid grid-cols-2 md:grid-cols-4 gap-6 max-w-6xl mx-auto">
          <div className="glass-card rounded-2xl px-6 py-8 text-center hover-lift">
            <div className="text-4xl mb-2">📚</div>
            <div className="text-3xl font-bold text-yellow-300 mb-2">1000+</div>
            <div className="text-yellow-200 font-semibold">كتاب متنوع</div>
          </div>
          <div className="glass-card rounded-2xl px-6 py-8 text-center hover-lift">
            <div className="text-4xl mb-2">👥</div>
            <div className="text-3xl font-bold text-yellow-300 mb-2">500+</div>
            <div className="text-yellow-200 font-semibold">عميل سعيد</div>
          </div>
          <div className="glass-card rounded-2xl px-6 py-8 text-center hover-lift">
            <div className="text-4xl mb-2">⭐</div>
            <div className="text-3xl font-bold text-yellow-300 mb-2">4.9</div>
            <div className="text-yellow-200 font-semibold">تقييم ممتاز</div>
          </div>
          <div className="glass-card rounded-2xl px-6 py-8 text-center hover-lift">
            <div className="text-4xl mb-2">🚚</div>
            <div className="text-3xl font-bold text-yellow-300 mb-2">24h</div>
            <div className="text-yellow-200 font-semibold">توصيل سريع</div>
          </div>
        </div>
      </div>
    </section>
  )
}

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
    <section className="py-24 bg-gradient-to-br from-gray-50 to-gray-100" dir="rtl">
      <div className="container mx-auto px-4">
        <div className="text-center mb-16">
          <h2 className="text-5xl font-bold gradient-text mb-6">✨ مميزاتنا الفريدة</h2>
          <p className="text-2xl text-gray-800 font-medium">🎯 نقدم لك تجربة شراء استثنائية</p>
        </div>
        
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
          {features.map((feature, index) => (
            <div key={index} className="neumorphism p-8 rounded-3xl hover-lift transition-all duration-500">
              <div className={`w-16 h-16 bg-gradient-to-r ${feature.color} rounded-2xl flex items-center justify-center text-black mb-6 mx-auto`}>
                {feature.icon}
              </div>
              <h3 className="text-2xl font-bold text-gray-900 mb-4 text-center">{feature.title}</h3>
              <p className="text-gray-700 text-center leading-relaxed">{feature.description}</p>
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
      return book.coverImageUrl
    }
    if (book.images && book.images.length > 0) {
      return book.images[0].imageUrl
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



// Login Modal Component with Glassmorphism
function LoginModal({ isOpen, onClose, onLogin }) {
  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')
  const [isLoading, setIsLoading] = useState(false)
  const [error, setError] = useState('')

  const handleSubmit = async (e) => {
    e.preventDefault()
    setIsLoading(true)
    setError('')

    try {
      const response = await apiService.login(email, password)
      onLogin(response.user)
      onClose()
      setEmail('')
      setPassword('')
    } catch (error) {
      console.error('Login error:', error)
      setError('فشل في تسجيل الدخول. يرجى التحقق من البيانات المدخلة.')
    } finally {
      setIsLoading(false)
    }
  }

  return (
    <Dialog open={isOpen} onOpenChange={onClose}>
      <DialogContent className="max-w-md glass-card" dir="rtl">
        <DialogHeader>
          <DialogTitle className="text-right text-2xl font-bold gradient-text">🔑 تسجيل الدخول</DialogTitle>
          <DialogDescription className="text-right text-gray-700">
            أدخل بياناتك للوصول إلى حسابك
          </DialogDescription>
        </DialogHeader>
        
        <form onSubmit={handleSubmit} className="space-y-6">
          {error && (
            <div className="neumorphism-inset px-4 py-3 rounded-xl text-red-700 text-right">
              {error}
            </div>
          )}
          
          <div className="space-y-3">
            <Label htmlFor="email" className="text-right block text-gray-800 font-semibold">البريد الإلكتروني</Label>
            <Input
              id="email"
              type="email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              placeholder="أدخل بريدك الإلكتروني"
              className="text-right neumorphism-inset border-0 focus:ring-2 focus:ring-yellow-400"
              required
            />
          </div>
          
          <div className="space-y-3">
            <Label htmlFor="password" className="text-right block text-gray-800 font-semibold">كلمة المرور</Label>
            <Input
              id="password"
              type="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              placeholder="أدخل كلمة المرور"
              className="text-right neumorphism-inset border-0 focus:ring-2 focus:ring-yellow-400"
              required
            />
          </div>
          
          <div className="flex gap-4 pt-6">
            <Button type="button" variant="outline" onClick={onClose} className="flex-1 neumorphism text-black hover:bg-yellow-400 rounded-xl transition-all duration-300">
              إلغاء
            </Button>
            <Button type="submit" disabled={isLoading} className="flex-1 neumorphism text-black hover:bg-yellow-400 rounded-xl transition-all duration-300">
              {isLoading ? 'جاري تسجيل الدخول...' : 'تسجيل الدخول'}
            </Button>
          </div>
        </form>
        
        <div className="text-center text-sm text-gray-600 mt-6 p-4 glass-card rounded-xl">
          <p className="font-semibold mb-2">للاختبار، استخدم:</p>
          <p>البريد الإلكتروني: admin@library.com</p>
          <p>كلمة المرور: Admin123!</p>
        </div>
      </DialogContent>
    </Dialog>
  )
}

// Footer Component with Aurora UI and Glassmorphism
function Footer() {
  return (
    <footer id="contact" className="aurora-bg text-white py-20 relative overflow-hidden" dir="rtl">
      {/* Aurora Background Elements */}
      <div className="absolute inset-0 opacity-10">
        <div className="absolute bottom-10 left-10 text-6xl float">📚</div>
        <div className="absolute bottom-20 right-20 text-5xl float" style={{animationDelay: '2s'}}>✨</div>
        <div className="absolute top-20 left-1/4 text-4xl float" style={{animationDelay: '4s'}}>🌟</div>
        <div className="absolute top-10 right-1/4 text-5xl float" style={{animationDelay: '6s'}}>📖</div>
      </div>
      
      <div className="container mx-auto px-4 relative z-10">
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-8 mb-12">
          {/* Library Info with Glassmorphism */}
          <div className="text-right">
            <div className="glass-card p-6 rounded-2xl hover-lift transition-all duration-500">
              <div className="flex items-center space-x-3 space-x-reverse mb-6">
                <div className="neumorphism p-3 rounded-xl">
                  <Book className="h-8 w-8 text-gradient-to-r from-yellow-500 to-yellow-600" />
                </div>
                <h3 className="text-2xl font-bold gradient-text">👑 ROYAL STUDY</h3>
              </div>
              <p className="text-yellow-200 mb-4 text-lg font-medium">
                📚 مكتبة إربد الأولى للكتب العربية
              </p>
              <p className="text-yellow-300 text-sm">
                نقدم أفضل الكتب العربية للأطفال واليافعين مع خدمة استثنائية
              </p>
            </div>
          </div>

          {/* Services with Neumorphism */}
          <div className="text-right">
            <div className="neumorphism-dark p-6 rounded-2xl hover-lift transition-all duration-500">
              <h4 className="text-xl font-bold mb-6 text-yellow-300">🎯 خدماتنا</h4>
              <ul className="space-y-4 text-yellow-200">
                <li className="flex items-center space-x-3 space-x-reverse hover:text-yellow-400 transition-colors">
                  <span className="text-2xl">📖</span>
                  <span className="font-medium text-lg">بيع الكتب</span>
                </li>
                <li className="flex items-center space-x-3 space-x-reverse hover:text-yellow-400 transition-colors">
                  <span className="text-2xl">🚚</span>
                  <span className="font-medium text-lg">التوصيل السريع</span>
                </li>
                <li className="flex items-center space-x-3 space-x-reverse hover:text-yellow-400 transition-colors">
                  <span className="text-2xl">💡</span>
                  <span className="font-medium text-lg">استشارات الكتب</span>
                </li>
                <li className="flex items-center space-x-3 space-x-reverse hover:text-yellow-400 transition-colors">
                  <span className="text-2xl">📋</span>
                  <span className="font-medium text-lg">الطلب المسبق</span>
                </li>
              </ul>
            </div>
          </div>

          {/* Quick Links with Glassmorphism */}
          <div className="text-right">
            <div className="glass-card p-6 rounded-2xl hover-lift transition-all duration-500">
              <h4 className="text-xl font-bold mb-6 text-yellow-300">🔗 روابط سريعة</h4>
              <ul className="space-y-4 text-yellow-200">
                <li className="flex items-center space-x-3 space-x-reverse hover:text-yellow-400 transition-colors">
                  <span className="text-2xl">⭐</span>
                  <a href="#featured-books" className="font-medium text-lg">الكتب المميزة</a>
                </li>
                <li className="flex items-center space-x-3 space-x-reverse hover:text-yellow-400 transition-colors">
                  <span className="text-2xl">🆕</span>
                  <a href="#new-releases" className="font-medium text-lg">الإصدارات الجديدة</a>
                </li>
                <li className="flex items-center space-x-3 space-x-reverse hover:text-yellow-400 transition-colors">
                  <span className="text-2xl">📚</span>
                  <a href="#all-books" className="font-medium text-lg">جميع الكتب</a>
                </li>
                <li className="flex items-center space-x-3 space-x-reverse hover:text-yellow-400 transition-colors">
                  <span className="text-2xl">✍️</span>
                  <a href="#" className="font-medium text-lg">المؤلفون</a>
                </li>
              </ul>
            </div>
          </div>

          {/* Contact with Neumorphism */}
          <div className="text-right">
            <div className="neumorphism-dark p-6 rounded-2xl hover-lift transition-all duration-500">
              <h4 className="text-xl font-bold mb-6 text-yellow-300">📞 تواصل معنا</h4>
              <div className="space-y-4 text-yellow-200">
                <div className="flex items-center space-x-3 space-x-reverse">
                  <div className="neumorphism p-2 rounded-xl">
                    <Phone className="h-5 w-5 text-yellow-400" />
                  </div>
                  <span className="font-medium text-lg">📱 +962785462983</span>
                </div>
                <div className="flex items-center space-x-3 space-x-reverse">
                  <div className="flex items-center space-x-3 space-x-reverse">
                    <div className="neumorphism p-2 rounded-xl">
                      <Mail className="h-5 w-5 text-yellow-400" />
                    </div>
                    <span className="font-medium text-lg">✉️ info@royalstudy.com</span>
                  </div>
                </div>
                <div className="flex items-center space-x-3 space-x-reverse">
                  <div className="neumorphism p-2 rounded-xl">
                    <MapPin className="h-5 w-5 text-yellow-400" />
                  </div>
                  <span className="font-medium text-lg">🏢 إربد، الأردن</span>
                </div>
                <div className="flex items-center space-x-3 space-x-reverse">
                  <div className="neumorphism p-2 rounded-xl">
                    <MessageCircle className="h-5 w-5 text-yellow-400" />
                  </div>
                  <span className="font-medium text-lg">💬 واتساب 24/7</span>
                </div>
              </div>
            </div>
          </div>
        </div>

        {/* Bottom Section with Glassmorphism - Black & Gold Theme */}
        <div className="glass-card p-8 rounded-2xl text-center">
          <div className="flex flex-col md:flex-row justify-between items-center space-y-4 md:space-y-0">
            <p className="text-yellow-200 text-lg font-medium">
              &copy; 2024 👑 ROYAL STUDY. جميع الحقوق محفوظة. ❤️
            </p>
            <div className="flex items-center space-x-4 space-x-reverse">
              <span className="text-yellow-300 font-semibold">تابعنا على:</span>
              <div className="flex space-x-3 space-x-reverse">
                <div className="neumorphism p-2 rounded-xl hover-lift cursor-pointer">
                  <span className="text-2xl">📱</span>
                </div>
                <div className="neumorphism p-2 rounded-xl hover-lift cursor-pointer">
                  <span className="text-2xl">📘</span>
                </div>
                <div className="neumorphism p-2 rounded-xl hover-lift cursor-pointer">
                  <span className="text-2xl">📷</span>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </footer>
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
  const [showLoginModal, setShowLoginModal] = useState(false)
  const [selectedCategoryForBooks, setSelectedCategoryForBooks] = useState('all')
  const [selectedAuthorForBooks, setSelectedAuthorForBooks] = useState('all')

  useEffect(() => {
    loadInitialData()
    setCurrentUser(apiService.getCurrentUser())
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
    setShowLoginModal(false)
    
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
    <div className="min-h-screen bg-gradient-to-br from-gray-50 via-gray-100 to-gray-200">
      <Header
        onSearch={handleGlobalSearch}
        currentUser={currentUser}
        onLogin={() => setShowLoginModal(true)}
        onLogout={handleLogout}
        onOpenAdmin={handleOpenAdmin}
        scrollToSection={scrollToSection}
      />
      
      <Routes>
        <Route path="/" element={
          <>
            <main>
              <HeroSection scrollToSection={scrollToSection} />
              
              {/* Features Section */}
              <FeaturesSection />
              
              {/* Featured Books Section with Aurora UI */}
              {featuredBooks.length > 0 && (
                <section id="featured-books" className="py-24 aurora-bg text-white relative overflow-hidden" dir="rtl">
                  {/* Aurora Background Elements */}
                  <div className="absolute inset-0 opacity-10">
                    <div className="absolute top-20 left-20 text-6xl float">⭐</div>
                    <div className="absolute bottom-20 right-20 text-5xl float" style={{animationDelay: '3s'}}>📚</div>
                    <div className="absolute top-1/2 right-1/4 text-4xl float" style={{animationDelay: '6s'}}>✨</div>
                  </div>
                  
                  <div className="container mx-auto px-4 relative z-10">
                    <div className="text-center mb-16">
                      <h2 className="text-6xl font-bold gradient-text mb-6 drop-shadow-2xl">⭐ الكتب المميزة</h2>
                      <div className="glass-card inline-block px-8 py-4 rounded-2xl">
                        <p className="text-3xl text-yellow-100 font-bold drop-shadow-lg">📚 اكتشف أفضل الكتب المختارة خصيصاً للأطفال</p>
                      </div>
                    </div>
                    <BooksGrid
                      books={featuredBooks}
                      loading={false}
                      onWhatsAppInquiry={handleWhatsAppInquiry}
                    />
                  </div>
                </section>
              )}

              {/* New Releases Section with Morphing Background */}
              {newReleases.length > 0 && (
                <section id="new-releases" className="py-24 morphing-bg text-white relative overflow-hidden" dir="rtl">
                  {/* Particle Effect */}
                  <div className="absolute inset-0 opacity-20">
                    <div className="absolute top-10 right-10 text-5xl float">🆕</div>
                    <div className="absolute bottom-10 left-10 text-6xl float" style={{animationDelay: '2s'}}>🎉</div>
                    <div className="absolute top-1/3 left-1/3 text-4xl float" style={{animationDelay: '4s'}}>📖</div>
                  </div>
                  
                  <div className="container mx-auto px-4 relative z-10">
                    <div className="text-center mb-16">
                      <h2 className="text-6xl font-bold gradient-text mb-6 drop-shadow-2xl">🆕 الإصدارات الجديدة</h2>
                      <div className="glass-card inline-block px-8 py-4 rounded-2xl">
                        <p className="text-3xl text-white font-bold drop-shadow-lg">🎉 آخر الكتب المثيرة التي وصلت إلى مكتبتنا</p>
                      </div>
                    </div>
                    <BooksGrid
                      books={newReleases}
                      loading={false}
                      onWhatsAppInquiry={handleWhatsAppInquiry}
                    />
                  </div>
                </section>
              )}

              {/* All Books Section with Neumorphism - Black & Gold Theme */}
              <section id="all-books" className="py-24 bg-gradient-to-br from-gray-50 to-gray-100" dir="rtl">
                <div className="container mx-auto px-4">
                  <div className="text-center mb-16">
                    <div className="neumorphism p-8 rounded-3xl inline-block">
                      <h2 className="text-6xl font-bold gradient-text mb-6 drop-shadow-2xl">
                        {searchTerm ? `🔍 نتائج البحث عن: "${searchTerm}"` : '📚 جميع الكتب'}
                      </h2>
                      <p className="text-3xl text-gray-800 font-bold">📖 تصفح مجموعتنا الكاملة من الكتب الرائعة</p>
                    </div>
                  </div>
                  <BooksGrid
                    books={books}
                    loading={loading}
                    onWhatsAppInquiry={handleWhatsAppInquiry}
                  />
                </div>
              </section>
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
      </Routes>

      {/* Login Modal */}
      <LoginModal
        isOpen={showLoginModal}
        onClose={() => setShowLoginModal(false)}
        onLogin={handleLogin}
      />
    </div>
  )
}

export default App

