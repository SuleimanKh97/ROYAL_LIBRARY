import { Button } from '@/components/ui/button';
import { BookOpen, Circle, Book, Calendar } from 'lucide-react';
import { Link } from 'react-router-dom';

const Hero = () => {

  return (
    <section className="relative overflow-hidden">
      {/* Hero Background */}
      <div className="bg-gradient-to-br from-[#EBB026] via-[#F4C430] to-[#FFD700] py-20 lg:py-32 relative">
        <div className="absolute inset-0 bg-gradient-to-br from-black/5 to-transparent"></div>
        <div className="container mx-auto px-4 text-center relative z-10">
          <div className="max-w-4xl mx-auto">
            {/* Logo */}
            <div className="mb-12 flex justify-center">
              <div className="w-24 h-24 lg:w-32 lg:h-32 flex items-center justify-center hover:scale-110 transition-transform duration-500">
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
            
            <h1 className="text-4xl lg:text-6xl font-black text-royal-black mb-6 tracking-tight">
              <span className="text-royal-black font-black">المكتبة الملكية</span>
              <br />
              <span className="text-2xl lg:text-4xl font-bold text-royal-black/80">Royal Study</span>
            </h1>
            <p className="text-xl lg:text-2xl text-royal-black/90 mb-8 font-medium leading-relaxed">
              وجهتك الأولى للتفوق الأكاديمي - دوسيات، كتب، قرطاسية وكورسات تعليمية
            </p>
            <p className="text-lg text-royal-black/80 mb-10 max-w-3xl mx-auto leading-relaxed">
              متخصصون في خدمة طلاب التوجيهي الأردني بأفضل المواد التعليمية والدعم الأكاديمي
            </p>
            
            <div className="flex flex-col sm:flex-row gap-4 justify-center items-center">
              <Button 
                size="lg"
                className="bg-royal-black hover:bg-royal-black/90 text-white font-bold px-8 py-4 text-lg rounded-xl shadow-xl hover:shadow-2xl hover-scale hover-glow transition-all duration-300"
                onClick={() => window.open('https://wa.me/962785462983?text=مرحباً، أرغب في الاستفسار عن خدماتكم', '_blank')}
              >
                ابدأ رحلتك التعليمية
              </Button>
              <Link to="/books">
                <Button 
                  size="lg"
                  variant="outline"
                  className="border-2 border-royal-black text-royal-black hover:bg-royal-black hover:text-white font-bold px-8 py-4 text-lg rounded-xl shadow-lg hover:shadow-xl hover-scale hover-glow transition-all duration-300"
                >
                  تصفح المنتجات
                </Button>
              </Link>
            </div>
          </div>
        </div>
      </div>

      {/* Quick Services Preview */}
      <div className="bg-royal-black py-16">
        <div className="container mx-auto px-4">
                     <div className="text-center mb-12">
             <h2 className="text-3xl lg:text-4xl font-black text-royal-gold mb-4">
               خدماتنا الأساسية
             </h2>
                           <p className="text-lg text-royal-gold max-w-2xl mx-auto">
                كل ما تحتاجه للتفوق الأكاديمي في مكان واحد
              </p>
           </div>

          <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
            {[
              {
                icon: Book,
                title: 'سوق الطلاب',
                description: 'كتب ودوسيات وقرطاسية',
                href: '/books',
                color: 'from-green-500 to-green-600'
              },
              {
                icon: Circle,
                title: 'كويزاتك',
                description: 'اختبر معلوماتك في جميع المواد',
                href: '/quizzes',
                color: 'from-blue-500 to-blue-600'
              },
              {
                icon: Calendar,
                title: 'الرزنامة الطلابية',
                description: 'جداول ومواعيد مهمة',
                href: '/calendar',
                color: 'from-purple-500 to-purple-600'
              }
            ].map((service, index) => {
              const Icon = service.icon;
              return (
                <Link
                  key={index}
                  to={service.href}
                  className="group"
                >
                  <div className="bg-white/10 backdrop-blur-sm rounded-xl p-6 hover:bg-white/20 transition-all duration-300 border border-royal-gold/20">
                    <div className={`w-12 h-12 bg-gradient-to-br ${service.color} rounded-lg flex items-center justify-center mb-4 group-hover:scale-110 transition-transform duration-300`}>
                      <Icon className="w-6 h-6 text-white" />
                    </div>
                                         <h3 className="text-xl font-bold text-royal-gold mb-2">
                       {service.title}
                     </h3>
                     <p className="text-royal-gold/80 text-sm">
                       {service.description}
                     </p>
                  </div>
                </Link>
              );
            })}
          </div>
        </div>
      </div>
    </section>
  );
};

export default Hero;
