const API_BASE_URL = 'http://localhost:5035/api';

// Helper: deep-convert object keys from PascalCase to camelCase
function isPlainObject(value) {
  return Object.prototype.toString.call(value) === '[object Object]';
}

function camelizeKey(key) {
  if (typeof key !== 'string' || key.length === 0) return key;
  // Only change keys that start with an uppercase ASCII letter
  return /^[A-Z]/.test(key) ? key.charAt(0).toLowerCase() + key.slice(1) : key;
}

function deepCamelize(value) {
  if (Array.isArray(value)) {
    return value.map((item) => deepCamelize(item));
  }
  if (isPlainObject(value)) {
    const result = {};
    for (const [key, val] of Object.entries(value)) {
      const newKey = camelizeKey(key);
      result[newKey] = deepCamelize(val);
    }
    return result;
  }
  return value;
}

class ApiService {
  constructor() {
    this.baseURL = API_BASE_URL;
  }

  // Helper method to get auth headers
  getAuthHeaders() {
    const token = localStorage.getItem('token');
    return {
      'Content-Type': 'application/json',
      ...(token && { 'Authorization': `Bearer ${token}` })
    };
  }

  // Helper method to get auth headers for file uploads
  getAuthHeadersForUpload() {
    const token = localStorage.getItem('token');
    return {
      ...(token && { 'Authorization': `Bearer ${token}` })
    };
  }

  // Upload image method
  async uploadImage(file) {
    try {
      const formData = new FormData();
      formData.append('file', file);

      const response = await fetch(`${this.baseURL}/Books/upload-image`, {
        method: 'POST',
        headers: this.getAuthHeadersForUpload(),
        body: formData
      });

      if (!response.ok) {
        const errorData = await response.json();
        throw new Error(errorData.message || `Upload failed: ${response.status}`);
      }

      return await response.json();
    } catch (error) {
      console.error('Upload error:', error);
      throw error;
    }
  }

  // Generic API call method
  async apiCall(endpoint, options = {}) {
    try {
      console.log(`Making API call to: ${this.baseURL}${endpoint}`)
      console.log('Options:', options)
      
      const response = await fetch(`${this.baseURL}${endpoint}`, {
        headers: this.getAuthHeaders(),
        ...options
      });

      console.log('Response status:', response.status)
      console.log('Response headers:', Object.fromEntries(response.headers.entries()))

      if (!response.ok) {
        let errorMessage = `API call failed: ${response.status} ${response.statusText}`
        
        try {
          const errorData = await response.json()
          console.error('Error response data:', errorData)
          errorMessage += `\nDetails: ${JSON.stringify(errorData)}`
        } catch (parseError) {
          console.error('Could not parse error response as JSON')
          const errorText = await response.text()
          errorMessage += `\nResponse text: ${errorText}`
        }
        
        const error = new Error(errorMessage)
        error.response = response
        throw error
      }

      const contentType = response.headers.get('content-type');
      if (contentType && contentType.includes('application/json')) {
        const rawResult = await response.json()
        const result = deepCamelize(rawResult)
        console.log('API call successful, result:', result)
        return result
      }
      
      const result = await response.text()
      console.log('API call successful, text result:', result)
      return result
    } catch (error) {
      console.error('API call error:', error)
      throw error
    }
  }

  // Authentication Methods
  async login(email, password) {
    const response = await this.apiCall('/Auth/login', {
      method: 'POST',
      body: JSON.stringify({ email, password })
    });
    
    if (response.token) {
      localStorage.setItem('token', response.token);
      localStorage.setItem('user', JSON.stringify(response.user));
      
      // Debug: Log what we're saving
      console.log('Login - saving user to localStorage:', response.user);
      console.log('Login - user role:', response.user?.role);
    }
    
    return response;
  }

  async logout() {
    try {
      await this.apiCall('/Auth/logout', { method: 'POST' });
    } catch (error) {
      console.error('Logout error:', error);
    } finally {
      localStorage.removeItem('token');
      localStorage.removeItem('user');
    }
  }

  getCurrentUser() {
    const user = localStorage.getItem('user');
    const parsedUser = user ? JSON.parse(user) : null;
    console.log('getCurrentUser() - raw user from localStorage:', user);
    console.log('getCurrentUser() - parsed user:', parsedUser);
    console.log('getCurrentUser() - user role:', parsedUser?.role);
    return parsedUser;
  }

  isAuthenticated() {
    return !!localStorage.getItem('token');
  }

  isAdmin() {
    const user = this.getCurrentUser();
    console.log('isAdmin() - user:', user);
    console.log('isAdmin() - user role:', user?.role);
    const isAdmin = user && (user.role === 'Admin' || user.role === 'Librarian');
    console.log('isAdmin() - result:', isAdmin);
    return isAdmin;
  }

  // Book Methods
  async getBooks(page = 1, pageSize = 12, searchTerm = '', categoryId = null, authorId = null) {
    const params = new URLSearchParams({
      page: page.toString(),
      pageSize: pageSize.toString(),
      ...(searchTerm && { searchTerm }),
      ...(categoryId && { categoryId: categoryId.toString() }),
      ...(authorId && { authorId: authorId.toString() })
    });

    return await this.apiCall(`/Books?${params}`);
  }

  async getBook(id) {
    return await this.apiCall(`/Books/${id}`);
  }

  async getFeaturedBooks() {
    return await this.apiCall('/Books/featured');
  }

  async getNewReleases() {
    return await this.apiCall('/Books/new-releases');
  }

  async getBooksByCategory(categoryId) {
    return await this.apiCall(`/Books/category/${categoryId}`);
  }

  async getBooksByAuthor(authorId) {
    return await this.apiCall(`/Books/author/${authorId}`);
  }

  // Category Methods
  async getCategories() {
    return await this.apiCall('/Categories/active');
  }

  async getCategory(id) {
    return await this.apiCall(`/Categories/${id}`);
  }

  // Author Methods
  async getAuthors() {
    return await this.apiCall('/Authors');
  }

  async getAuthor(id) {
    return await this.apiCall(`/Authors/${id}`);
  }

  // Publisher Methods
  async getPublishers() {
    return await this.apiCall('/Publishers');
  }

  async getPublisher(id) {
    return await this.apiCall(`/Publishers/${id}`);
  }

  // Dashboard Methods
  async getDashboardStats() {
    return await this.apiCall('/Dashboard/stats');
  }

  // Admin Methods - Books
  async createBook(bookData) {
    return await this.apiCall('/Books', {
      method: 'POST',
      body: JSON.stringify(bookData)
    });
  }

  async updateBook(id, bookData) {
    return await this.apiCall(`/Books/${id}`, {
      method: 'PUT',
      body: JSON.stringify(bookData)
    });
  }

  async deleteBook(id) {
    return await this.apiCall(`/Books/${id}`, {
      method: 'DELETE'
    });
  }

  async updateBookStock(id, quantity) {
    return await this.apiCall(`/Books/${id}/stock`, {
      method: 'PATCH',
      body: JSON.stringify({ quantity })
    });
  }

  // Admin Methods - Authors
  async createAuthor(authorData) {
    return await this.apiCall('/Authors', {
      method: 'POST',
      body: JSON.stringify(authorData)
    });
  }

  async updateAuthor(id, authorData) {
    return await this.apiCall(`/Authors/${id}`, {
      method: 'PUT',
      body: JSON.stringify(authorData)
    });
  }

  async deleteAuthor(id) {
    return await this.apiCall(`/Authors/${id}`, {
      method: 'DELETE'
    });
  }

  // Admin Methods - Categories
  async createCategory(categoryData) {
    return await this.apiCall('/Categories', {
      method: 'POST',
      body: JSON.stringify(categoryData)
    });
  }

  async updateCategory(id, categoryData) {
    return await this.apiCall(`/Categories/${id}`, {
      method: 'PUT',
      body: JSON.stringify(categoryData)
    });
  }

  async deleteCategory(id) {
    return await this.apiCall(`/Categories/${id}`, {
      method: 'DELETE'
    });
  }

  // Admin Methods - Publishers
  async createPublisher(publisherData) {
    return await this.apiCall('/Publishers', {
      method: 'POST',
      body: JSON.stringify(publisherData)
    });
  }

  async updatePublisher(id, publisherData) {
    return await this.apiCall(`/Publishers/${id}`, {
      method: 'PUT',
      body: JSON.stringify(publisherData)
    });
  }

  async deletePublisher(id) {
    return await this.apiCall(`/Publishers/${id}`, {
      method: 'DELETE'
    });
  }

  // Admin Methods - Book Inquiries
  async createBookInquiry(inquiryData) {
    return await this.apiCall('/BookInquiries', {
      method: 'POST',
      body: JSON.stringify(inquiryData)
    });
  }

  async updateBookInquiry(id, inquiryData) {
    return await this.apiCall(`/BookInquiries/${id}`, {
      method: 'PUT',
      body: JSON.stringify(inquiryData)
    });
  }

  async deleteBookInquiry(id) {
    return await this.apiCall(`/BookInquiries/${id}`, {
      method: 'DELETE'
    });
  }

  async getBookInquiries(page = 1, pageSize = 20) {
    const params = new URLSearchParams({
      page: page.toString(),
      pageSize: pageSize.toString()
    });
    return await this.apiCall(`/BookInquiries?${params}`);
  }

  async getWhatsAppUrl(bookId, customerData) {
    return await this.apiCall('/BookInquiries/whatsapp-url', {
      method: 'POST',
      body: JSON.stringify({ bookId, ...customerData })
    });
  }

  async registerCustomer(registerData) {
    return await this.apiCall('/Auth/register-customer', {
      method: 'POST',
      body: JSON.stringify(registerData)
    });
  }
}

// Create and export a singleton instance
export default new ApiService();

