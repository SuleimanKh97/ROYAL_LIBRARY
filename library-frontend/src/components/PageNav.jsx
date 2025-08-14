import React from 'react'
import { useNavigate } from 'react-router-dom'
import { Button } from './ui/button'
import { Home, Brain, BarChart2, Phone, ArrowLeft } from 'lucide-react'

export default function PageNav({ showBack = true, className = '' }) {
  const navigate = useNavigate()
  
  const go = (path) => {
    if (!path) return
    navigate(path)
  }

  return (
    <div className={`mb-6 flex flex-wrap gap-2 ${className}`}>
      <Button variant="outline" onClick={() => go('/')}> 
        <Home className="h-4 w-4 ml-2" /> الرئيسية
      </Button>
      <Button variant="outline" onClick={() => go('/quizzes')}>
        <Brain className="h-4 w-4 ml-2" /> الكويزات
      </Button>
      <Button variant="outline" onClick={() => go('/my-attempts')}>
        <BarChart2 className="h-4 w-4 ml-2" /> محاولاتي
      </Button>
      <Button variant="outline" onClick={() => go('/contact')}>
        <Phone className="h-4 w-4 ml-2" /> اتصل بنا
      </Button>
      {showBack && (
        <Button variant="outline" onClick={() => window.history.back()}>
          <ArrowLeft className="h-4 w-4 ml-2" /> رجوع
        </Button>
      )}
    </div>
  )
}


