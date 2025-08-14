import { useState, useEffect } from 'react'
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card.jsx'
import { Button } from '@/components/ui/button.jsx'
import { Input } from '@/components/ui/input.jsx'
import { Badge } from '@/components/ui/badge.jsx'
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '@/components/ui/select.jsx'
import { Dialog, DialogContent, DialogDescription, DialogHeader, DialogTitle } from '@/components/ui/dialog.jsx'
import { 
  Book, 
  Search, 
  Filter,
  Grid,
  List,
  Star,
  ShoppingCart,
  Eye,
  MessageCircle,
  Calendar,
  User,
  Tag,
  ArrowLeft
} from 'lucide-react'
import apiService, { fixImageUrl } from '../lib/api.js'

export default function BooksPage({ onBack, onWhatsAppInquiry, onViewDetails, initialCategory, initialAuthor }) {
  const [books, setBooks] = useState([])
  const [categories, setCategories] = useState([])
  const [authors, setAuthors] = useState([])
  const [loading, setLoading] = useState(true)
  const [searchTerm, setSearchTerm] = useState('')
  const [selectedCategory, setSelectedCategory] = useState(initialCategory || 'all')
  const [selectedAuthor, setSelectedAuthor] = useState(initialAuthor || 'all')
  const [viewMode, setViewMode] = useState('grid') // 'grid' or 'list'
  const [currentPage, setCurrentPage] = useState(1)
  const [totalPages, setTotalPages] = useState(1)
  const [selectedBook, setSelectedBook] = useState(null)
  const [showBookDetails, setShowBookDetails] = useState(false)

  useEffect(() => {
    loadData()
  }, [currentPage, selectedCategory, selectedAuthor])

  // Update selectedCategory when initialCategory prop changes
  useEffect(() => {
    if (initialCategory && initialCategory !== selectedCategory) {
      setSelectedCategory(initialCategory)
    } else if (!initialCategory && selectedCategory !== 'all') {
      setSelectedCategory('all')
    }
  }, [initialCategory])

  // Update selectedAuthor when initialAuthor prop changes
  useEffect(() => {
    if (initialAuthor && initialAuthor !== selectedAuthor) {
      setSelectedAuthor(initialAuthor)
    } else if (!initialAuthor && selectedAuthor !== 'all') {
      setSelectedAuthor('all')
    }
  }, [initialAuthor])

  const loadData = async () => {
    setLoading(true)
    try {
      // Build API request parameters
      const params = {
        page: currentPage,
        pageSize: 12
      }

      if (searchTerm) {
        params.search = searchTerm
      }

      if (selectedCategory && selectedCategory !== 'all') {
        params.categoryId = selectedCategory
      }

      if (selectedAuthor && selectedAuthor !== 'all') {
        params.authorId = selectedAuthor
      }

      // Fetch data from API
      const [booksResponse, categoriesResponse, authorsResponse] = await Promise.all([
        apiService.getBooks(params.page, params.pageSize, params.search, params.categoryId, params.authorId),
        apiService.getCategories(),
        apiService.getAuthors()
      ])

      if (booksResponse && booksResponse.items) {
        setBooks(booksResponse.items)
        setTotalPages(Math.ceil(booksResponse.totalCount / params.pageSize))
      } else {
        setBooks([])
        setTotalPages(1)
      }

      if (categoriesResponse) {
        setCategories(categoriesResponse)
      } else {
        setCategories([])
      }

      if (authorsResponse) {
        setAuthors(authorsResponse)
      } else {
        setAuthors([])
      }
      
    } catch (error) {
      console.error('Error loading books data:', error)
      setBooks([])
      setCategories([])
      setAuthors([])
      setTotalPages(1)
    } finally {
      setLoading(false)
    }
  }

  const handleSearch = async () => {
    setCurrentPage(1)
    await loadData()
  }

  const handleCategoryChange = (categoryId) => {
    setSelectedCategory(categoryId)
    setCurrentPage(1)
  }

  const handleAuthorChange = (authorId) => {
    setSelectedAuthor(authorId)
    setCurrentPage(1)
  }

  const getBookImage = (book) => {
    // Debug: Log book data
    console.log('Book data for image in BooksPage:', {
      id: book.id,
      title: book.title,
      coverImageUrl: book.coverImageUrl,
      images: book.images
    })
    
    // First try coverImageUrl
    if (book.coverImageUrl) {
      const fixedUrl = fixImageUrl(book.coverImageUrl);
      console.log('Using coverImageUrl:', fixedUrl)
      return fixedUrl
    }
    // Then try images array
    if (book.images && book.images.length > 0) {
      const fixedUrl = fixImageUrl(book.images[0].imageUrl);
      console.log('Using images[0].imageUrl:', fixedUrl)
      return fixedUrl
    }
    // Fallback to placeholder
    console.log('Using placeholder image')
    return 'data:image/svg+xml;base64,PHN2ZyB3aWR0aD0iMzAwIiBoZWlnaHQ9IjQwMCIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIj4KICA8cmVjdCB3aWR0aD0iMzAwIiBoZWlnaHQ9IjQwMCIgZmlsbD0iI2YwZjBmMCIvPgogIDx0ZXh0IHg9IjE1MCIgeT0iMjAwIiBmb250LWZhbWlseT0iQXJpYWwiIGZvbnQtc2l6ZT0iMjAiIGZpbGw9IiM2NjY2NjYiIHRleHQtYW5jaG9yPSJtaWRkbGUiIGR5PSIuM2VtIj7Zhtin2YbYqDwvdGV4dD4KPC9zdmc+'
  }

  const handleImageError = (event) => {
    console.log('Image failed to load:', event.target.src);
    // Set fallback image
    event.target.src = 'data:image/svg+xml;base64,PHN2ZyB3aWR0aD0iMzAwIiBoZWlnaHQ9IjQwMCIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIj4KICA8cmVjdCB3aWR0aD0iMzAwIiBoZWlnaHQ9IjQwMCIgZmlsbD0iI2YwZjBmMCIvPgogIDx0ZXh0IHg9IjE1MCIgeT0iMjAwIiBmb250LWZhbWlseT0iQXJpYWwiIGZvbnQtc2l6ZT0iMjAiIGZpbGw9IiM2NjY2NjYiIHRleHQtYW5jaG9yPSJtaWRkbGUiIGR5PSIuM2VtIj7Zhtin2YbYqDwvdGV4dD4KPC9zdmc+';
  }

  const getBookTitle = (book) => {
    return book.titleArabic || book.title || 'عنوان غير محدد'
  }

  const getAuthorName = (book) => {
    return book.authorNameArabic || book.authorName || book.author?.nameArabic || book.author?.name || 'مؤلف غير محدد'
  }

  const getCategoryName = (book) => {
    return book.categoryNameArabic || book.categoryName || book.category?.nameArabic || book.category?.name || 'تصنيف غير محدد'
  }

  const getPrice = (book) => {
    return book.price ? `${book.price} د.أ` : 'غير متوفر'
  }

  const isAvailable = (book) => {
    return book.stockQuantity > 0
  }

  const handleViewDetails = (book) => {
    setSelectedBook(book)
    setShowBookDetails(true)
  }

  return (
    <div className="min-h-screen bg-gradient-to-br from-gray-50 via-gray-100 to-gray-200" dir="rtl">
      {/* Header */}
      <div className="bg-gradient-to-r from-black via-gray-900 to-gray-800 shadow-xl border-b-4 border-yellow-400">
        <div className="container mx-auto px-4 py-8">
          <div className="flex items-center justify-between mb-8">
            <div className="flex items-center space-x-4 space-x-reverse">
              <Button variant="outline" size="sm" onClick={onBack} className="flex items-center bg-gradient-to-r from-yellow-400 to-yellow-500 hover:from-yellow-500 hover:to-yellow-600 text-black font-bold border-2 border-yellow-600 rounded-xl shadow-lg hover:shadow-xl transition-all duration-300 hover:scale-105">
                <ArrowLeft className="h-4 w-4 ml-2" />
                🏠 العودة للرئيسية
              </Button>
              <div className="flex items-center space-x-3 space-x-reverse">
                <div className="p-2 bg-gradient-to-br from-yellow-400 to-yellow-500 rounded-xl shadow-lg">
                  <Book className="h-8 w-8 text-black" />
                </div>
                <h1 className="text-4xl font-bold text-yellow-100 drop-shadow-lg">📚 جميع الكتب</h1>
              </div>
            </div>
            <div className="flex items-center space-x-3 space-x-reverse">
              <Button
                variant={viewMode === 'grid' ? 'default' : 'outline'}
                size="sm"
                onClick={() => setViewMode('grid')}
                className={`${viewMode === 'grid' ? 'bg-gradient-to-r from-yellow-400 to-yellow-500 text-black' : 'bg-yellow-100 text-black hover:bg-yellow-200'} font-bold border-2 border-yellow-400 rounded-xl shadow-md hover:shadow-lg transition-all duration-300`}
              >
                <Grid className="h-4 w-4" />
              </Button>
              <Button
                variant={viewMode === 'list' ? 'default' : 'outline'}
                size="sm"
                onClick={() => setViewMode('list')}
                className={`${viewMode === 'list' ? 'bg-gradient-to-r from-yellow-400 to-yellow-500 text-black' : 'bg-yellow-100 text-black hover:bg-yellow-200'} font-bold border-2 border-yellow-400 rounded-xl shadow-md hover:shadow-lg transition-all duration-300`}
              >
                <List className="h-4 w-4" />
              </Button>
            </div>
          </div>

          {/* Search and Filters */}
          <div className="grid grid-cols-1 md:grid-cols-4 gap-6 bg-gradient-to-r from-yellow-200 to-yellow-300 p-6 rounded-2xl shadow-xl border-2 border-yellow-400">
            <div className="md:col-span-2">
              <div className="relative">
                <Search className="absolute right-4 top-1/2 transform -translate-y-1/2 text-yellow-600 h-5 w-5" />
                <Input
                  type="text"
                  placeholder="🔍 ابحث عن كتابك المفضل..."
                  value={searchTerm}
                  onChange={(e) => setSearchTerm(e.target.value)}
                  onKeyPress={(e) => e.key === 'Enter' && handleSearch()}
                  className="pr-12 text-right bg-white border-2 border-yellow-400 focus:border-yellow-500 focus:shadow-lg transition-all duration-300 rounded-xl font-medium text-black placeholder:text-yellow-600"
                />
              </div>
            </div>
            <Select value={selectedCategory} onValueChange={handleCategoryChange}>
              <SelectTrigger className="bg-white border-2 border-yellow-400 focus:border-yellow-500 rounded-xl font-bold text-black">
                <SelectValue placeholder="🏷️ اختر التصنيف" />
              </SelectTrigger>
              <SelectContent className="bg-white border-2 border-yellow-400 rounded-xl">
                <SelectItem value="all" className="font-bold text-black hover:bg-yellow-100">📚 جميع التصنيفات</SelectItem>
                {categories.map((category) => (
                  <SelectItem key={category.id} value={category.id.toString()} className="font-medium text-black hover:bg-yellow-100">
                    {category.nameArabic || category.name}
                  </SelectItem>
                ))}
              </SelectContent>
            </Select>
            <Select value={selectedAuthor} onValueChange={handleAuthorChange}>
              <SelectTrigger className="bg-white border-2 border-yellow-400 focus:border-yellow-500 rounded-xl font-bold text-black">
                <SelectValue placeholder="✍️ اختر المؤلف" />
              </SelectTrigger>
              <SelectContent className="bg-white border-2 border-yellow-400 rounded-xl">
                <SelectItem value="all" className="font-bold text-black hover:bg-yellow-100">👥 جميع المؤلفين</SelectItem>
                {authors.map((author) => (
                  <SelectItem key={author.id} value={author.id.toString()} className="font-medium text-black hover:bg-yellow-100">
                    {author.nameArabic || author.name}
                  </SelectItem>
                ))}
              </SelectContent>
            </Select>
          </div>
        </div>
      </div>

      {/* Content */}
      <div className="container mx-auto px-4 py-12">
        {loading ? (
          <div className="text-center py-20">
            <div className="animate-spin rounded-full h-16 w-16 border-b-4 border-yellow-500 mx-auto"></div>
            <p className="mt-6 text-2xl text-gray-800 font-bold">📚 جاري تحميل الكتب الرائعة...</p>
          </div>
        ) : books.length === 0 ? (
          <div className="text-center py-20 bg-gradient-to-r from-yellow-100 to-yellow-200 rounded-2xl shadow-xl border-2 border-yellow-400">
            <div className="p-8">
              <Book className="h-24 w-24 text-yellow-600 mx-auto mb-6" />
              <h3 className="text-3xl font-bold text-gray-900 mb-4">😔 لا توجد كتب</h3>
              <p className="text-xl text-gray-700 font-medium">لم يتم العثور على كتب تطابق معايير البحث</p>
            </div>
          </div>
        ) : (
          <>
            {/* Books Grid/List */}
            <div className={viewMode === 'grid' ? 'grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-8' : 'space-y-6'}>
              {books.map((book) => (
                <Card key={book.id} className={`${viewMode === 'list' ? 'flex' : ''} bg-gradient-to-br from-white to-yellow-50 border-2 border-yellow-300 shadow-xl hover:shadow-2xl transition-all duration-300 hover:scale-105 rounded-2xl overflow-hidden`}>
                  <div className={viewMode === 'list' ? 'flex-shrink-0 w-40' : ''}>
                    <img
                      src={getBookImage(book)}
                      alt={getBookTitle(book)}
                      className={`w-full object-cover ${viewMode === 'list' ? 'h-40' : 'h-72'} transition-transform duration-300 hover:scale-110`}
                      onError={handleImageError}
                    />
                  </div>
                  <CardContent className={viewMode === 'list' ? 'flex-1 p-6' : 'p-6'}>
                    <div className="space-y-3">
                      <h3 className="font-bold text-xl line-clamp-2 text-black">{getBookTitle(book)}</h3>
                      
                      <div className="flex items-center space-x-2 space-x-reverse text-yellow-700">
                        <User className="h-5 w-5" />
                        <span className="font-medium text-lg">✍️ {getAuthorName(book)}</span>
                      </div>
                      
                      <div className="flex items-center space-x-2 space-x-reverse text-yellow-700">
                        <Tag className="h-5 w-5" />
                        <span className="font-medium text-lg">🏷️ {getCategoryName(book)}</span>
                      </div>
                      
                      <div className="flex items-center justify-between">
                        <Badge variant={isAvailable(book) ? 'default' : 'secondary'} className={`${isAvailable(book) ? 'bg-gradient-to-r from-green-400 to-green-500 text-white' : 'bg-gradient-to-r from-red-400 to-red-500 text-white'} font-bold text-lg px-4 py-2 rounded-xl shadow-md`}>
                          {isAvailable(book) ? '✅ متوفر' : '❌ غير متوفر'}
                        </Badge>
                        <span className="font-bold text-2xl text-yellow-700">💰 {getPrice(book)}</span>
                      </div>
                      
                      {book.isFeatured && (
                        <div className="flex items-center space-x-2 space-x-reverse bg-gradient-to-r from-yellow-200 to-yellow-300 px-3 py-2 rounded-xl">
                          <Star className="h-5 w-5 text-yellow-600 fill-current" />
                          <span className="font-bold text-yellow-700">⭐ كتاب مميز</span>
                        </div>
                      )}
                      
                      <div className="flex items-center space-x-3 space-x-reverse pt-4">
                        <Button
                          size="sm"
                          variant="outline"
                          onClick={() => handleViewDetails(book)}
                          className="flex-1 bg-gradient-to-r from-yellow-400 to-yellow-500 hover:from-yellow-500 hover:to-yellow-600 text-black font-bold border-2 border-yellow-600 rounded-xl shadow-lg hover:shadow-xl transition-all duration-300"
                        >
                          <Eye className="h-4 w-4 ml-1" />
                          👁️ التفاصيل
                         </Button>
                        <Button
                          size="sm"
                          onClick={() => onWhatsAppInquiry(book)}
                          className="flex-1 bg-gradient-to-r from-green-400 to-green-500 hover:from-green-500 hover:to-green-600 text-white font-bold border-2 border-green-600 rounded-xl shadow-lg hover:shadow-xl transition-all duration-300"
                          disabled={!isAvailable(book)}
                        >
                          <MessageCircle className="h-4 w-4 ml-1" />
                          💬 استفسار
                        </Button>
                      </div>
                    </div>
                  </CardContent>
                </Card>
              ))}
            </div>

            {/* Pagination */}
            {totalPages > 1 && (
              <div className="flex justify-center mt-12">
                <div className="flex items-center space-x-3 space-x-reverse bg-gradient-to-r from-yellow-200 to-yellow-300 p-4 rounded-2xl shadow-xl border-2 border-yellow-400">
                  <Button
                    variant="outline"
                    size="sm"
                    onClick={() => setCurrentPage(Math.max(1, currentPage - 1))}
                    disabled={currentPage === 1}
                    className="bg-gradient-to-r from-yellow-400 to-yellow-500 hover:from-yellow-500 hover:to-yellow-600 text-black font-bold border-2 border-yellow-600 rounded-xl shadow-lg hover:shadow-xl transition-all duration-300 disabled:opacity-50"
                  >
                    ⬅️ السابق
                  </Button>
                  
                  {Array.from({ length: Math.min(5, totalPages) }, (_, i) => {
                    const page = Math.max(1, Math.min(totalPages - 4, currentPage - 2)) + i
                    return (
                      <Button
                        key={page}
                        variant={currentPage === page ? 'default' : 'outline'}
                        size="sm"
                        onClick={() => setCurrentPage(page)}
                        className={`${currentPage === page ? 'bg-gradient-to-r from-black to-yellow-900 text-yellow-300' : 'bg-white text-black hover:bg-yellow-100'} font-bold border-2 border-yellow-600 rounded-xl shadow-lg hover:shadow-xl transition-all duration-300`}
                      >
                        {page}
                      </Button>
                    )
                  })}
                  
                  <Button
                    variant="outline"
                    size="sm"
                    onClick={() => setCurrentPage(Math.min(totalPages, currentPage + 1))}
                    disabled={currentPage === totalPages}
                    className="bg-gradient-to-r from-yellow-400 to-yellow-500 hover:from-yellow-500 hover:to-yellow-600 text-black font-bold border-2 border-yellow-600 rounded-xl shadow-lg hover:shadow-xl transition-all duration-300 disabled:opacity-50"
                  >
                    التالي ➡️
                  </Button>
                </div>
              </div>
            )}
          </>
        )}
      </div>

      {/* Book Details Modal */}
      <BookDetailsModal
        book={selectedBook}
        isOpen={showBookDetails}
        onClose={() => {
          setShowBookDetails(false)
          setSelectedBook(null)
        }}
        onWhatsAppInquiry={onWhatsAppInquiry}
      />
    </div>
  )
}

// Book Details Modal Component
function BookDetailsModal({ book, isOpen, onClose, onWhatsAppInquiry }) {
  if (!book) return null

  const getBookImage = () => {
    if (book.coverImageUrl) {
      return fixImageUrl(book.coverImageUrl)
    }
    if (book.images && book.images.length > 0) {
      return fixImageUrl(book.images[0].imageUrl)
    }
    return 'data:image/svg+xml;base64,PHN2ZyB3aWR0aD0iNDAwIiBoZWlnaHQ9IjYwMCIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIj4KICA8cmVjdCB3aWR0aD0iNDAwIiBoZWlnaHQ9IjYwMCIgZmlsbD0iI2YwZjBmMCIvPgogIDx0ZXh0IHg9IjIwMCIgeT0iMzAwIiBmb250LWZhbWlseT0iQXJpYWwiIGZvbnQtc2l6ZT0iMjAiIGZpbGw9IiM2NjY2NjYiIHRleHQtYW5jaG9yPSJtaWRkbGUiIGR5PSIuM2VtIj7Zhtin2YbYqDwvdGV4dD4KPC9zdmc+'
  }

  const handleImageError = (event) => {
    console.log('Modal image failed to load:', event.target.src);
    // Set fallback image
    event.target.src = 'data:image/svg+xml;base64,PHN2ZyB3aWR0aD0iNDAwIiBoZWlnaHQ9IjYwMCIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIj4KICA8cmVjdCB3aWR0aD0iNDAwIiBoZWlnaHQ9IjYwMCIgZmlsbD0iI2YwZjBmMCIvPgogIDx0ZXh0IHg9IjIwMCIgeT0iMzAwIiBmb250LWZhbWlseT0iQXJpYWwiIGZvbnQtc2l6ZT0iMjAiIGZpbGw9IiM2NjY2NjYiIHRleHQtYW5jaG9yPSJtaWRkbGUiIGR5PSIuM2VtIj7Zhtin2YbYqDwvdGV4dD4KPC9zdmc+';
  }

  return (
    <Dialog open={isOpen} onOpenChange={onClose}>
      <DialogContent className="max-w-4xl max-h-[90vh] overflow-y-auto" dir="rtl">
        <DialogHeader>
          <DialogTitle className="text-right text-2xl">
            {book.titleArabic || book.title}
          </DialogTitle>
        </DialogHeader>
        
        <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
          {/* Book Image */}
          <div className="aspect-[3/4] bg-gray-100 rounded-lg overflow-hidden">
            <img
              src={getBookImage()}
              alt={book.titleArabic || book.title}
              className="w-full h-full object-cover"
              onError={handleImageError}
            />
          </div>
          
          {/* Book Details */}
          <div className="space-y-4 text-right">
            <div>
              <h3 className="text-lg font-semibold mb-2">معلومات الكتاب</h3>
              <div className="space-y-2">
                <p><span className="font-medium">العنوان:</span> {book.titleArabic || book.title}</p>
                <p><span className="font-medium">المؤلف:</span> {book.authorNameArabic || book.authorName || book.author?.nameArabic || book.author?.name || 'غير محدد'}</p>
                <p><span className="font-medium">التصنيف:</span> {book.categoryNameArabic || book.categoryName || book.category?.nameArabic || book.category?.name || 'غير محدد'}</p>
                <p><span className="font-medium">الناشر:</span> {book.publisherNameArabic || book.publisherName || book.publisher?.nameArabic || book.publisher?.name || 'غير محدد'}</p>
                <p><span className="font-medium">السعر:</span> {book.price ? `${book.price} د.أ` : 'غير محدد'}</p>
                <p><span className="font-medium">المخزون:</span> {book.stockQuantity || 0} نسخة</p>
                <p><span className="font-medium">الحالة:</span> 
                  <Badge variant={book.isAvailable && book.stockQuantity > 0 ? "default" : "destructive"} className="mr-2">
                    {book.isAvailable && book.stockQuantity > 0 ? 'متوفر' : 'غير متوفر'}
                  </Badge>
                </p>
              </div>
            </div>
            
            {book.descriptionArabic || book.description ? (
              <div>
                <h3 className="text-lg font-semibold mb-2">وصف الكتاب</h3>
                <p className="text-gray-700 leading-relaxed">
                  {book.descriptionArabic || book.description}
                </p>
              </div>
            ) : null}
            
            <div className="flex gap-2 pt-4">
              <Button
                onClick={() => {
                  onWhatsAppInquiry(book)
                  onClose()
                }}
                className="flex-1 bg-gradient-to-r from-green-400 to-green-500 hover:from-green-500 hover:to-green-600 text-white font-bold border-2 border-green-600 rounded-xl shadow-lg hover:shadow-xl transition-all duration-300"
              >
                <MessageCircle className="h-4 w-4 ml-1" />
                استفسار واتساب
              </Button>
            </div>
          </div>
        </div>
      </DialogContent>
    </Dialog>
  )
} 