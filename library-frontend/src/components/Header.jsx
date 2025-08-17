import { useState } from 'react';
import { Link, useLocation } from 'react-router-dom';
import { Button } from '@/components/ui/button';
import { BookOpen, Book, Calendar, Circle, X, Award, Lightbulb } from 'lucide-react';

const Header = ({ onLogin, onRegister }) => {
  const [isMenuOpen, setIsMenuOpen] = useState(false);
  const location = useLocation();

  const navigation = [
    { name: 'الرئيسية', href: '/', icon: BookOpen },
    { name: 'كويزاتك', href: '/quizzes', icon: Circle },
    { name: 'سوق الطلاب', href: '/books', icon: Book },
    { name: 'دليل النجاح', href: '/success-guide', icon: Award },
    { name: 'نصائح وإرشادات', href: '/study-tips', icon: Lightbulb },
    { name: 'الرزنامة الطلابية', href: '/calendar', icon: Calendar },
  ];

  const isActive = (path) => location.pathname === path;

  const handleLogin = () => {
    if (onLogin) {
      onLogin();
    }
  };

  const handleRegister = () => {
    if (onRegister) {
      onRegister();
    } else {
      window.location.href = '/register';
    }
  };

  return (
    <header className="bg-royal-black text-white shadow-lg relative z-50 border-b border-royal-gold/20">
      <div className="container mx-auto px-4">
        <div className="flex items-center justify-between h-18">
          {/* Logo */}
          <Link to="/" className="flex items-center space-x-3 space-x-reverse group">
            <div className="w-16 h-16 bg-gradient-to-br from-royal-gold to-yellow-500 rounded-xl flex items-center justify-center shadow-lg group-hover:scale-110 transition-transform duration-300 overflow-hidden">
              <img 
                src="/royal-study-logo.png" 
                alt="ROYAL STUDY Logo" 
                className="w-14 h-14 object-contain"
                onError={(e) => {
                  e.target.style.display = 'none';
                  e.target.nextSibling.style.display = 'flex';
                }}
              />
              <BookOpen className="w-8 h-8 text-royal-black hidden" />
            </div>
            <div>
              <h1 className="text-2xl font-black text-royal-gold">
                Royal Study
              </h1>
              <p className="text-xs text-royal-gold/80 font-semibold">مكتبة الملكية للدراسة</p>
            </div>
          </Link>

          {/* Desktop Navigation */}
          <nav className="hidden lg:flex items-center space-x-6 space-x-reverse">
            {navigation.map((item) => {
              const Icon = item.icon;
              return (
                <Link
                  key={item.name}
                  to={item.href}
                  className={`flex items-center space-x-2 space-x-reverse px-4 py-3 rounded-xl text-sm font-bold transition-all duration-300 ${
                    isActive(item.href)
                      ? 'bg-gradient-to-r from-royal-gold to-yellow-500 text-royal-black shadow-lg scale-105'
                      : 'text-white hover:text-royal-gold hover:bg-white/10 hover:scale-105'
                  }`}
                >
                  <Icon className="w-4 h-4" />
                  <span>{item.name}</span>
                </Link>
              );
            })}
          </nav>

          {/* Desktop Actions */}
          <div className="hidden lg:flex items-center space-x-4 space-x-reverse">
            {/* WhatsApp Button */}
            <Button 
              className="bg-gradient-to-r from-royal-gold to-yellow-500 hover:from-yellow-500 hover:to-royal-gold text-royal-black font-bold shadow-lg hover:shadow-xl transform hover:scale-105 transition-all duration-300 rounded-xl px-6"
              onClick={() => window.open('https://wa.me/962791234567', '_blank')}
            >
              تواصل معنا
            </Button>

            {/* Login Button */}
            <Button 
              variant="outline"
              className="border-2 border-royal-gold text-royal-gold hover:bg-royal-gold hover:text-royal-black font-bold shadow-lg hover:shadow-xl transform hover:scale-105 transition-all duration-300 rounded-xl px-6"
              onClick={handleLogin}
            >
              تسجيل الدخول
            </Button>

            {/* Register Button */}
            <Button 
              className="bg-gradient-to-r from-green-500 to-green-600 hover:from-green-600 hover:to-green-700 text-white font-bold shadow-lg hover:shadow-xl transform hover:scale-105 transition-all duration-300 rounded-xl px-6"
              onClick={handleRegister}
            >
              التسجيل
            </Button>

            {/* Admin Panel Button */}
            <Button 
              variant="outline"
              className="border-2 border-purple-500 text-purple-600 hover:bg-purple-500 hover:text-white font-bold shadow-lg hover:shadow-xl transform hover:scale-105 transition-all duration-300 rounded-xl px-6"
              onClick={() => window.location.href = '/admin/calendar'}
            >
              لوحة الإدارة
            </Button>
          </div>

          {/* Mobile menu button */}
          <button
            className="lg:hidden p-2 hover:bg-white/10 rounded-lg transition-colors duration-300"
            onClick={() => setIsMenuOpen(!isMenuOpen)}
          >
            {isMenuOpen ? (
              <X className="w-6 h-6" />
            ) : (
              <div className="w-6 h-6 flex flex-col justify-center space-y-1">
                <div className="w-full h-0.5 bg-white rounded-full"></div>
                <div className="w-full h-0.5 bg-white rounded-full"></div>
                <div className="w-full h-0.5 bg-white rounded-full"></div>
              </div>
            )}
          </button>
        </div>

        {/* Mobile Navigation */}
        {isMenuOpen && (
          <div className="lg:hidden absolute top-full left-0 right-0 bg-royal-black/95 backdrop-blur-sm border-t border-royal-gold/20 shadow-lg">
            <nav className="py-6 px-4 space-y-3">
              {navigation.map((item) => {
                const Icon = item.icon;
                return (
                  <Link
                    key={item.name}
                    to={item.href}
                    className={`flex items-center space-x-3 space-x-reverse px-4 py-4 rounded-xl text-sm font-bold transition-all duration-300 ${
                      isActive(item.href)
                        ? 'bg-gradient-to-r from-royal-gold to-yellow-500 text-royal-black shadow-lg'
                        : 'text-white hover:text-royal-gold hover:bg-white/10'
                    }`}
                    onClick={() => setIsMenuOpen(false)}
                  >
                    <Icon className="w-5 h-5" />
                    <span>{item.name}</span>
                  </Link>
                );
              })}
              
              {/* Mobile Action Buttons */}
              <div className="pt-4 border-t border-royal-gold/20 space-y-3">
                <Button 
                  className="w-full bg-gradient-to-r from-royal-gold to-yellow-500 hover:from-yellow-500 hover:to-royal-gold text-royal-black font-bold rounded-xl shadow-lg hover:shadow-xl transform hover:scale-105 transition-all duration-300"
                  onClick={() => {
                    window.open('https://wa.me/962791234567', '_blank');
                    setIsMenuOpen(false);
                  }}
                >
                  تواصل معنا عبر واتساب
                </Button>
                
                <Button 
                  variant="outline"
                  className="w-full border-2 border-royal-gold text-royal-gold hover:bg-royal-gold hover:text-royal-black font-bold rounded-xl shadow-lg hover:shadow-xl transform hover:scale-105 transition-all duration-300"
                  onClick={() => {
                    handleLogin();
                    setIsMenuOpen(false);
                  }}
                >
                  تسجيل الدخول
                </Button>
                
                                 <Button 
                   className="w-full bg-gradient-to-r from-green-500 to-green-600 hover:from-green-600 hover:to-green-700 text-white font-bold rounded-xl shadow-lg hover:shadow-xl transform hover:scale-105 transition-all duration-300"
                   onClick={() => {
                     handleRegister();
                     setIsMenuOpen(false);
                   }}
                 >
                   التسجيل
                 </Button>
                 
                 <Button 
                   variant="outline"
                   className="w-full border-2 border-purple-500 text-purple-600 hover:bg-purple-500 hover:text-white font-bold rounded-xl shadow-lg hover:shadow-xl transform hover:scale-105 transition-all duration-300"
                   onClick={() => {
                     window.location.href = '/admin/calendar';
                     setIsMenuOpen(false);
                   }}
                 >
                   لوحة الإدارة
                 </Button>
              </div>
            </nav>
          </div>
        )}
      </div>
    </header>
  );
};

export default Header;
