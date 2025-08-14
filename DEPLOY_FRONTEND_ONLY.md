# 🚀 Frontend-Only Deployment Guide

## Quick Start (5 Minutes)

### Step 1: Start Your Backend
```bash
cd LibraryManagementAPI
dotnet run
```
Your API will be running on `http://localhost:5035`

### Step 2: Create ngrok Tunnel
Open a **new terminal** and run:
```bash
ngrok http 5035
```
You'll see a URL like: `https://abc123.ngrok.io`

### Step 3: Deploy to Vercel
1. Go to [vercel.com](https://vercel.com)
2. Sign in with GitHub
3. Click "New Project"
4. Import your repository
5. Configure:
   - **Framework Preset**: Vite
   - **Root Directory**: `library-frontend`
   - **Build Command**: `npm run build`
   - **Output Directory**: `dist`
   - **Install Command**: `npm install`

### Step 4: Set Environment Variable
In Vercel project settings, add:
```
VITE_API_BASE_URL=https://YOUR_NGROK_URL/api
```
Replace `YOUR_NGROK_URL` with the ngrok URL from Step 2.

### Step 5: Deploy
Click "Deploy" and wait for build to complete.

## Testing

✅ **Your Vercel app will now work from anywhere!**
- Frontend: Hosted on Vercel
- Backend: Running locally via ngrok tunnel

## Important Notes

⚠️ **Keep Running:**
- Your backend (`dotnet run`) must stay running
- Your ngrok tunnel must stay active
- If you restart either, you'll need to update the Vercel environment variable

🔄 **Updates:**
- Frontend changes: Automatically redeploy to Vercel
- Backend changes: Restart `dotnet run` and update ngrok URL

## Troubleshooting

### ngrok URL Changed?
If ngrok gives you a new URL:
1. Update the environment variable in Vercel
2. Redeploy the project

### Backend Not Responding?
1. Check if `dotnet run` is still running
2. Check if ngrok tunnel is active
3. Test: `curl https://your-ngrok-url/api/Books`

### CORS Errors?
Your backend may need CORS configuration. Add this to your backend's `Program.cs`:
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

## Next Steps

When ready for production:
1. Deploy backend to Railway/Azure
2. Update `VITE_API_BASE_URL` to production URL
3. Remove ngrok dependency
