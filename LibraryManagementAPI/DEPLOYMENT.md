# 🚀 Backend API Deployment Guide

## Overview
This .NET 8.0 Web API needs to be deployed to a cloud service before the frontend can be deployed to Vercel.

## Deployment Options

### Option 1: Azure App Service (Recommended)

#### Prerequisites
- Azure account
- Azure CLI installed
- .NET 8.0 SDK

#### Steps
1. **Install Azure CLI**
   ```bash
   # Windows
   winget install -e --id Microsoft.AzureCLI
   
   # Or download from: https://docs.microsoft.com/en-us/cli/azure/install-azure-cli
   ```

2. **Login to Azure**
   ```bash
   az login
   ```

3. **Create Resource Group**
   ```bash
   az group create --name RoyalLibraryRG --location "East US"
   ```

4. **Create App Service Plan**
   ```bash
   az appservice plan create --name RoyalLibraryPlan --resource-group RoyalLibraryRG --sku B1 --is-linux
   ```

5. **Create Web App**
   ```bash
   az webapp create --name royal-library-api --resource-group RoyalLibraryRG --plan RoyalLibraryPlan --runtime "DOTNETCORE:8.0"
   ```

6. **Configure Database Connection**
   ```bash
   # Add your database connection string
   az webapp config appsettings set --name royal-library-api --resource-group RoyalLibraryRG --settings "ConnectionStrings__DefaultConnection"="your-connection-string"
   ```

7. **Deploy the Application**
   ```bash
   # From the LibraryManagementAPI directory
   az webapp deployment source config-zip --resource-group RoyalLibraryRG --name royal-library-api --src ./publish.zip
   ```

### Option 2: Railway (Easier)

#### Steps
1. Go to [railway.app](https://railway.app)
2. Sign up with GitHub
3. Create new project
4. Connect your GitHub repository
5. Set root directory to `LibraryManagementAPI`
6. Add environment variables:
   - `ConnectionStrings__DefaultConnection`
   - `JWT__SecretKey`
   - `JWT__Issuer`
   - `JWT__Audience`

### Option 3: Render

#### Steps
1. Go to [render.com](https://render.com)
2. Sign up with GitHub
3. Create new Web Service
4. Connect your repository
5. Configure:
   - **Name**: royal-library-api
   - **Root Directory**: LibraryManagementAPI
   - **Build Command**: `dotnet publish -c Release -o out`
   - **Start Command**: `dotnet out/LibraryManagementAPI.dll`
   - **Environment**: .NET

## Environment Variables

Set these environment variables in your deployment platform:

```env
# Database
ConnectionStrings__DefaultConnection=your-database-connection-string

# JWT Configuration
JWT__SecretKey=your-secret-key-here
JWT__Issuer=your-app-name
JWT__Audience=your-app-name

# CORS (for frontend)
AllowedOrigins=https://your-frontend-domain.vercel.app
```

## Database Setup

### Option A: Azure SQL Database
1. Create Azure SQL Database
2. Update connection string
3. Run migrations

### Option B: MySQL on Railway/Render
1. Add MySQL service
2. Update connection string
3. Run migrations

## CORS Configuration

Update `Program.cs` to allow your Vercel frontend:

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVercel", policy =>
    {
        policy.WithOrigins("https://your-app.vercel.app")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
```

## Testing Deployment

After deployment, test your API:
```bash
curl https://your-api-domain.com/api/Books
```

## Update Frontend

Once your API is deployed, update the frontend environment variable:
```env
VITE_API_BASE_URL=https://your-api-domain.com/api
```

## Troubleshooting

### Common Issues
1. **Database Connection**: Ensure connection string is correct
2. **CORS Errors**: Check CORS configuration
3. **JWT Issues**: Verify JWT secret and configuration
4. **File Uploads**: Ensure wwwroot/uploads directory is writable

### Logs
Check application logs in your deployment platform for errors.

## Cost Estimation

- **Azure App Service**: ~$13/month (B1 tier)
- **Railway**: Free tier available, then $5/month
- **Render**: Free tier available, then $7/month
