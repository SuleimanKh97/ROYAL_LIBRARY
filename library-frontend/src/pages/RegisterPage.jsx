import { useState } from 'react'
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card.jsx'
import { Button } from '@/components/ui/button.jsx'
import { Input } from '@/components/ui/input.jsx'
import { Label } from '@/components/ui/label.jsx'
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '@/components/ui/select.jsx'
import { 
  User, 
  Mail, 
  Lock, 
  Phone, 
  Calendar,
  MapPin,
  ArrowLeft,
  CheckCircle,
  AlertCircle
} from 'lucide-react'
import apiService from '../lib/api.js'

export default function RegisterPage({ onBack, onLoginSuccess }) {
  const [formData, setFormData] = useState({
    firstName: '',
    lastName: '',
    email: '',
    username: '',
    password: '',
    confirmPassword: '',
    phoneNumber: '',
    dateOfBirth: '',
    gender: '',
    address: ''
  })

  const [errors, setErrors] = useState({})
  const [loading, setLoading] = useState(false)
  const [success, setSuccess] = useState(false)

  const handleInputChange = (field, value) => {
    setFormData(prev => ({
      ...prev,
      [field]: value
    }))
    
    // Clear error when user starts typing
    if (errors[field]) {
      setErrors(prev => ({
        ...prev,
        [field]: ''
      }))
    }
  }

  const validateForm = () => {
    const newErrors = {}

    // First Name
    if (!formData.firstName.trim()) {
      newErrors.firstName = 'الاسم الأول مطلوب'
    } else if (!/^[a-zA-Z\u0600-\u06FF\s]+$/.test(formData.firstName)) {
      newErrors.firstName = 'الاسم الأول يجب أن يحتوي على أحرف فقط'
    }

    // Last Name
    if (!formData.lastName.trim()) {
      newErrors.lastName = 'الاسم الأخير مطلوب'
    } else if (!/^[a-zA-Z\u0600-\u06FF\s]+$/.test(formData.lastName)) {
      newErrors.lastName = 'الاسم الأخير يجب أن يحتوي على أحرف فقط'
    }

    // Email
    if (!formData.email.trim()) {
      newErrors.email = 'البريد الإلكتروني مطلوب'
    } else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(formData.email)) {
      newErrors.email = 'البريد الإلكتروني غير صحيح'
    }

    // Username
    if (!formData.username.trim()) {
      newErrors.username = 'اسم المستخدم مطلوب'
    } else if (!/^[a-zA-Z0-9_]+$/.test(formData.username)) {
      newErrors.username = 'اسم المستخدم يجب أن يحتوي على أحرف إنجليزية وأرقام وشرطة سفلية فقط'
    }

    // Password
    if (!formData.password) {
      newErrors.password = 'كلمة المرور مطلوبة'
    } else if (formData.password.length < 8) {
      newErrors.password = 'كلمة المرور يجب أن تكون 8 أحرف على الأقل'
    } else if (!/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/.test(formData.password)) {
      newErrors.password = 'كلمة المرور يجب أن تحتوي على حرف كبير وحرف صغير ورقم ورمز خاص'
    }

    // Confirm Password
    if (!formData.confirmPassword) {
      newErrors.confirmPassword = 'تأكيد كلمة المرور مطلوب'
    } else if (formData.password !== formData.confirmPassword) {
      newErrors.confirmPassword = 'كلمة المرور وتأكيدها غير متطابقين'
    }

    // Phone Number
    if (!formData.phoneNumber.trim()) {
      newErrors.phoneNumber = 'رقم الهاتف مطلوب'
    } else if (!/^[+]?[0-9\s\-\(\)]{10,15}$/.test(formData.phoneNumber)) {
      newErrors.phoneNumber = 'رقم الهاتف غير صحيح'
    }

    // Date of Birth
    if (!formData.dateOfBirth) {
      newErrors.dateOfBirth = 'تاريخ الميلاد مطلوب'
    } else {
      const age = new Date().getFullYear() - new Date(formData.dateOfBirth).getFullYear()
      if (age < 13) {
        newErrors.dateOfBirth = 'يجب أن يكون عمرك 13 سنة على الأقل'
      }
    }

    // Gender
    if (!formData.gender) {
      newErrors.gender = 'الجنس مطلوب'
    } else if (!['ذكر', 'أنثى'].includes(formData.gender)) {
      newErrors.gender = 'الجنس يجب أن يكون ذكر أو أنثى'
    }

    setErrors(newErrors)
    return Object.keys(newErrors).length === 0
  }

  const handleSubmit = async (e) => {
    e.preventDefault()
    
    if (!validateForm()) {
      return
    }

    setLoading(true)
    setErrors({})

    try {
      // Convert dateOfBirth to proper format for backend
      const registrationData = {
        ...formData,
        dateOfBirth: formData.dateOfBirth ? new Date(formData.dateOfBirth) : null
      }
      
      const result = await apiService.registerCustomer(registrationData)
      
      if (result && result.token) {
        setSuccess(true)
        
        // Auto login after successful registration
        setTimeout(() => {
          onLoginSuccess(result.user, result.token)
        }, 2000)
      } else {
        setErrors({ general: 'فشل في إنشاء الحساب. يرجى المحاولة مرة أخرى.' })
      }
    } catch (error) {
      console.error('Registration error:', error)
      
      if (error.response) {
        const errorData = error.response
        if (errorData.message) {
          setErrors({ general: errorData.message })
        } else if (errorData.errors) {
          // Handle validation errors from backend
          const backendErrors = {}
          Object.keys(errorData.errors).forEach(key => {
            backendErrors[key] = errorData.errors[key][0]
          })
          setErrors(backendErrors)
        } else {
          setErrors({ general: 'حدث خطأ أثناء التسجيل' })
        }
      } else {
        setErrors({ general: 'حدث خطأ في الاتصال بالخادم' })
      }
    } finally {
      setLoading(false)
    }
  }

  if (success) {
    return (
      <div className="min-h-screen bg-gradient-to-br from-green-50 to-green-100 flex items-center justify-center p-4" dir="rtl">
        <Card className="w-full max-w-md bg-white shadow-2xl border-2 border-green-300">
          <CardContent className="p-8 text-center">
            <CheckCircle className="h-16 w-16 text-green-500 mx-auto mb-4" />
            <h2 className="text-2xl font-bold text-green-800 mb-4">🎉 تم التسجيل بنجاح!</h2>
            <p className="text-green-700 mb-6">مرحباً بك في مكتبتنا الرقمية</p>
            <div className="animate-pulse">
              <p className="text-sm text-green-600">جاري تسجيل الدخول تلقائياً...</p>
            </div>
          </CardContent>
        </Card>
      </div>
    )
  }

  return (
    <div className="min-h-screen bg-gradient-to-br from-gray-50 via-gray-100 to-gray-200" dir="rtl">
      <div className="container mx-auto px-4 pt-4">
        <div className="flex gap-2 mb-2">
          <button onClick={() => window.location.href='/'} className="text-sm underline">الرئيسية</button>
          <button onClick={() => window.location.href='/quizzes'} className="text-sm underline">الكويزات</button>
          <button onClick={() => window.location.href='/my-attempts'} className="text-sm underline">محاولاتي</button>
          <button onClick={() => window.history.back()} className="text-sm underline">رجوع</button>
        </div>
      </div>
      {/* Header */}
      <div className="bg-gradient-to-r from-black via-amber-900 to-amber-800 shadow-xl border-b-4 border-amber-400">
        <div className="container mx-auto px-4 py-8">
          <div className="flex items-center justify-between">
            <div className="flex items-center space-x-4 space-x-reverse">
              <Button variant="outline" size="sm" onClick={onBack} className="flex items-center bg-gradient-to-r from-amber-400 to-amber-500 hover:from-amber-500 hover:to-amber-600 text-black font-bold border-2 border-amber-600 rounded-xl shadow-lg hover:shadow-xl transition-all duration-300 hover:scale-105">
                <ArrowLeft className="h-4 w-4 ml-2" />
                🏠 العودة للرئيسية
              </Button>
              <div className="flex items-center space-x-3 space-x-reverse">
                <div className="p-2 bg-gradient-to-br from-amber-400 to-amber-500 rounded-xl shadow-lg">
                  <User className="h-8 w-8 text-black" />
                </div>
                <h1 className="text-4xl font-bold text-amber-100 drop-shadow-lg">📝 تسجيل حساب جديد</h1>
              </div>
            </div>
          </div>
        </div>
      </div>

      {/* Registration Form */}
      <div className="container mx-auto px-4 py-12">
        <div className="max-w-2xl mx-auto">
          <Card className="bg-gradient-to-br from-white to-amber-50 border-2 border-amber-300 shadow-2xl">
            <CardHeader className="text-center pb-6">
              <CardTitle className="text-3xl font-bold text-amber-900">🎯 انضم إلى مكتبتنا الرقمية</CardTitle>
              <CardDescription className="text-lg text-amber-700">
                سجل حسابك الجديد واستمتع بقراءة الكتب الرائعة
              </CardDescription>
            </CardHeader>
            
            <CardContent className="p-8">
              {errors.general && (
                <div className="mb-6 p-4 bg-red-50 border-2 border-red-300 rounded-xl">
                  <div className="flex items-center space-x-2 space-x-reverse">
                    <AlertCircle className="h-5 w-5 text-red-500" />
                    <span className="text-red-700 font-medium">{errors.general}</span>
                  </div>
                </div>
              )}

              <form onSubmit={handleSubmit} className="space-y-6">
                {/* Personal Information */}
                <div className="bg-amber-50 p-6 rounded-xl border-2 border-amber-200">
                  <h3 className="text-xl font-bold text-amber-900 mb-4 flex items-center">
                    👤 المعلومات الشخصية
                  </h3>
                  <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                    <div className="space-y-2">
                      <Label htmlFor="firstName" className="text-lg font-bold text-amber-800">
                        الاسم الأول *
                      </Label>
                      <Input
                        id="firstName"
                        value={formData.firstName}
                        onChange={(e) => handleInputChange('firstName', e.target.value)}
                        className={`text-lg p-4 border-2 ${errors.firstName ? 'border-red-400' : 'border-amber-300'} focus:border-amber-500 rounded-xl bg-white focus:bg-amber-50 transition-all duration-300`}
                        placeholder="أدخل اسمك الأول..."
                      />
                      {errors.firstName && (
                        <p className="text-red-600 text-sm">{errors.firstName}</p>
                      )}
                    </div>

                    <div className="space-y-2">
                      <Label htmlFor="lastName" className="text-lg font-bold text-amber-800">
                        الاسم الأخير *
                      </Label>
                      <Input
                        id="lastName"
                        value={formData.lastName}
                        onChange={(e) => handleInputChange('lastName', e.target.value)}
                        className={`text-lg p-4 border-2 ${errors.lastName ? 'border-red-400' : 'border-amber-300'} focus:border-amber-500 rounded-xl bg-white focus:bg-amber-50 transition-all duration-300`}
                        placeholder="أدخل اسمك الأخير..."
                      />
                      {errors.lastName && (
                        <p className="text-red-600 text-sm">{errors.lastName}</p>
                      )}
                    </div>

                    <div className="space-y-2">
                      <Label htmlFor="dateOfBirth" className="text-lg font-bold text-amber-800">
                        تاريخ الميلاد *
                      </Label>
                      <Input
                        id="dateOfBirth"
                        type="date"
                        value={formData.dateOfBirth}
                        onChange={(e) => handleInputChange('dateOfBirth', e.target.value)}
                        className={`text-lg p-4 border-2 ${errors.dateOfBirth ? 'border-red-400' : 'border-amber-300'} focus:border-amber-500 rounded-xl bg-white focus:bg-amber-50 transition-all duration-300`}
                      />
                      {errors.dateOfBirth && (
                        <p className="text-red-600 text-sm">{errors.dateOfBirth}</p>
                      )}
                    </div>

                    <div className="space-y-2">
                      <Label htmlFor="gender" className="text-lg font-bold text-amber-800">
                        الجنس *
                      </Label>
                      <Select value={formData.gender} onValueChange={(value) => handleInputChange('gender', value)}>
                        <SelectTrigger className={`text-lg p-4 border-2 ${errors.gender ? 'border-red-400' : 'border-amber-300'} focus:border-amber-500 rounded-xl bg-white focus:bg-amber-50`}>
                          <SelectValue placeholder="اختر الجنس..." />
                        </SelectTrigger>
                        <SelectContent className="bg-white border-2 border-amber-300 rounded-xl">
                          <SelectItem value="ذكر" className="text-lg hover:bg-amber-100">👨 ذكر</SelectItem>
                          <SelectItem value="أنثى" className="text-lg hover:bg-amber-100">👩 أنثى</SelectItem>
                        </SelectContent>
                      </Select>
                      {errors.gender && (
                        <p className="text-red-600 text-sm">{errors.gender}</p>
                      )}
                    </div>
                  </div>
                </div>

                {/* Contact Information */}
                <div className="bg-amber-50 p-6 rounded-xl border-2 border-amber-200">
                  <h3 className="text-xl font-bold text-amber-900 mb-4 flex items-center">
                    📞 معلومات الاتصال
                  </h3>
                  <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                    <div className="space-y-2">
                      <Label htmlFor="email" className="text-lg font-bold text-amber-800">
                        البريد الإلكتروني *
                      </Label>
                      <Input
                        id="email"
                        type="email"
                        value={formData.email}
                        onChange={(e) => handleInputChange('email', e.target.value)}
                        className={`text-lg p-4 border-2 ${errors.email ? 'border-red-400' : 'border-amber-300'} focus:border-amber-500 rounded-xl bg-white focus:bg-amber-50 transition-all duration-300`}
                        placeholder="example@email.com"
                      />
                      {errors.email && (
                        <p className="text-red-600 text-sm">{errors.email}</p>
                      )}
                    </div>

                    <div className="space-y-2">
                      <Label htmlFor="phoneNumber" className="text-lg font-bold text-amber-800">
                        رقم الهاتف *
                      </Label>
                      <Input
                        id="phoneNumber"
                        value={formData.phoneNumber}
                        onChange={(e) => handleInputChange('phoneNumber', e.target.value)}
                        className={`text-lg p-4 border-2 ${errors.phoneNumber ? 'border-red-400' : 'border-amber-300'} focus:border-amber-500 rounded-xl bg-white focus:bg-amber-50 transition-all duration-300`}
                        placeholder="+962785462983"
                      />
                      {errors.phoneNumber && (
                        <p className="text-red-600 text-sm">{errors.phoneNumber}</p>
                      )}
                    </div>

                    <div className="md:col-span-2 space-y-2">
                      <Label htmlFor="address" className="text-lg font-bold text-amber-800">
                        العنوان
                      </Label>
                      <Input
                        id="address"
                        value={formData.address}
                        onChange={(e) => handleInputChange('address', e.target.value)}
                        className="text-lg p-4 border-2 border-amber-300 focus:border-amber-500 rounded-xl bg-white focus:bg-amber-50 transition-all duration-300"
                        placeholder="أدخل عنوانك..."
                      />
                    </div>
                  </div>
                </div>

                {/* Account Information */}
                <div className="bg-amber-50 p-6 rounded-xl border-2 border-amber-200">
                  <h3 className="text-xl font-bold text-amber-900 mb-4 flex items-center">
                    🔐 معلومات الحساب
                  </h3>
                  <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                    <div className="space-y-2">
                      <Label htmlFor="username" className="text-lg font-bold text-amber-800">
                        اسم المستخدم *
                      </Label>
                      <Input
                        id="username"
                        value={formData.username}
                        onChange={(e) => handleInputChange('username', e.target.value)}
                        className={`text-lg p-4 border-2 ${errors.username ? 'border-red-400' : 'border-amber-300'} focus:border-amber-500 rounded-xl bg-white focus:bg-amber-50 transition-all duration-300`}
                        placeholder="username123"
                      />
                      {errors.username && (
                        <p className="text-red-600 text-sm">{errors.username}</p>
                      )}
                    </div>

                    <div className="space-y-2">
                      <Label htmlFor="password" className="text-lg font-bold text-amber-800">
                        كلمة المرور *
                      </Label>
                      <Input
                        id="password"
                        type="password"
                        value={formData.password}
                        onChange={(e) => handleInputChange('password', e.target.value)}
                        className={`text-lg p-4 border-2 ${errors.password ? 'border-red-400' : 'border-amber-300'} focus:border-amber-500 rounded-xl bg-white focus:bg-amber-50 transition-all duration-300`}
                        placeholder="كلمة المرور..."
                      />
                      {errors.password && (
                        <p className="text-red-600 text-sm">{errors.password}</p>
                      )}
                    </div>

                    <div className="md:col-span-2 space-y-2">
                      <Label htmlFor="confirmPassword" className="text-lg font-bold text-amber-800">
                        تأكيد كلمة المرور *
                      </Label>
                      <Input
                        id="confirmPassword"
                        type="password"
                        value={formData.confirmPassword}
                        onChange={(e) => handleInputChange('confirmPassword', e.target.value)}
                        className={`text-lg p-4 border-2 ${errors.confirmPassword ? 'border-red-400' : 'border-amber-300'} focus:border-amber-500 rounded-xl bg-white focus:bg-amber-50 transition-all duration-300`}
                        placeholder="أعد إدخال كلمة المرور..."
                      />
                      {errors.confirmPassword && (
                        <p className="text-red-600 text-sm">{errors.confirmPassword}</p>
                      )}
                    </div>
                  </div>

                  <div className="mt-4 p-4 bg-blue-50 border-2 border-blue-200 rounded-xl">
                    <h4 className="font-bold text-blue-800 mb-2">🔒 متطلبات كلمة المرور:</h4>
                    <ul className="text-sm text-blue-700 space-y-1">
                      <li>• 8 أحرف على الأقل</li>
                      <li>• حرف كبير واحد على الأقل</li>
                      <li>• حرف صغير واحد على الأقل</li>
                      <li>• رقم واحد على الأقل</li>
                      <li>• رمز خاص واحد على الأقل (@$!%*?&)</li>
                    </ul>
                  </div>
                </div>

                {/* Submit Button */}
                <div className="flex gap-4 pt-6">
                  <Button 
                    type="button" 
                    variant="outline" 
                    onClick={onBack}
                    className="flex-1 px-8 py-4 text-lg font-bold bg-gradient-to-r from-gray-400 to-gray-500 hover:from-gray-500 hover:to-gray-600 text-white border-2 border-gray-600 rounded-xl shadow-lg hover:shadow-xl transition-all duration-300 hover:scale-105"
                  >
                    ❌ إلغاء
                  </Button>
                  <Button 
                    type="submit"
                    disabled={loading}
                    className="flex-1 px-8 py-4 text-lg font-bold bg-gradient-to-r from-green-400 to-green-500 hover:from-green-500 hover:to-green-600 text-white border-2 border-green-600 rounded-xl shadow-lg hover:shadow-xl transition-all duration-300 hover:scale-105 disabled:opacity-50"
                  >
                    {loading ? (
                      <div className="flex items-center">
                        <div className="animate-spin rounded-full h-5 w-5 border-2 border-white border-t-transparent ml-2"></div>
                        جاري التسجيل...
                      </div>
                    ) : (
                      <>
                        <User className="h-5 w-5 ml-2" />
                        📝 تسجيل الحساب
                      </>
                    )}
                  </Button>
                </div>
              </form>
            </CardContent>
          </Card>
        </div>
      </div>
    </div>
  )
} 