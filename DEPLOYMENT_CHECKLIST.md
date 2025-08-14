# 🚀 Complete Deployment Checklist

## Phase 1: Backend API Deployment

### Step 1: Choose Backend Platform
- [ ] **Railway** (Recommended - Easiest)
- [ ] **Azure App Service** (Best for .NET)
- [ ] **Render** (Free tier available)

### Step 2: Deploy Backend
**If using Railway:**
- [ ] Go to [railway.app](https://railway.app)
- [ ] Sign up with GitHub
- [ ] Create new project
- [ ] Connect your repository
- [ ] Set root directory to `LibraryManagementAPI`
- [ ] Add environment variables:
  - [ ] `ConnectionStrings__DefaultConnection`
  - [ ] `JWT__SecretKey`
  - [ ] `JWT__Issuer`
  - [ ] `JWT__Audience`
- [ ] Deploy and get API URL

### Step 3: Test Backend
- [ ] Test API endpoint: `https://your-api-domain.com/api/Books`
- [ ] Verify CORS is configured for Vercel domains
- [ ] Test authentication endpoints

## Phase 2: Frontend Deployment

### Step 4: Prepare Repository
- [ ] Push code to GitHub/GitLab/Bitbucket
- [ ] Ensure all files are committed

### Step 5: Deploy to Vercel
- [ ] Go to [vercel.com](https://vercel.com)
- [ ] Sign in with GitHub
- [ ] Click "New Project"
- [ ] Import your repository
- [ ] Configure project:
  - [ ] Framework Preset: Vite
  - [ ] Root Directory: `library-frontend`
  - [ ] Build Command: `npm run build`
  - [ ] Output Directory: `dist`
  - [ ] Install Command: `npm install`

### Step 6: Set Environment Variables
- [ ] Add `VITE_API_BASE_URL` = `https://your-backend-api-url.com/api`
- [ ] Deploy

### Step 7: Test Frontend
- [ ] Test all major functionality
- [ ] Verify API communication
- [ ] Test authentication
- [ ] Test file uploads

## Phase 3: Post-Deployment

### Step 8: Domain Setup (Optional)
- [ ] Add custom domain in Vercel
- [ ] Configure DNS settings
- [ ] Update CORS in backend for custom domain

### Step 9: Monitoring
- [ ] Set up Vercel Analytics
- [ ] Monitor error logs
- [ ] Test performance

## Troubleshooting Checklist

### Backend Issues
- [ ] Database connection working?
- [ ] CORS configured correctly?
- [ ] JWT tokens working?
- [ ] File uploads working?

### Frontend Issues
- [ ] API calls successful?
- [ ] Environment variables set?
- [ ] Build completing successfully?
- [ ] Routing working correctly?

## Quick Commands

### Test Backend Locally
```bash
cd LibraryManagementAPI
dotnet run
```

### Test Frontend Locally
```bash
cd library-frontend
npm run dev
```

### Build Frontend
```bash
cd library-frontend
npm run build
```

## Environment Variables Reference

### Backend (.NET)
```env
ConnectionStrings__DefaultConnection=your-db-connection
JWT__SecretKey=your-secret-key
JWT__Issuer=your-app-name
JWT__Audience=your-app-name
```

### Frontend (Vercel)
```env
VITE_API_BASE_URL=https://your-backend-api-url.com/api
```

## Cost Estimation

### Monthly Costs
- **Backend (Railway)**: $5-10/month
- **Frontend (Vercel)**: Free tier
- **Database**: $5-20/month (depending on provider)
- **Total**: ~$10-30/month

## Success Criteria

- [ ] Backend API accessible and responding
- [ ] Frontend deployed and loading
- [ ] Authentication working
- [ ] All CRUD operations functional
- [ ] File uploads working
- [ ] Quiz system operational
- [ ] Admin dashboard accessible
