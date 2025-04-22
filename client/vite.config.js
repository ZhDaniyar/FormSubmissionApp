import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'

export default defineConfig({
  plugins: [vue()],
  build: {
    outDir: 'dist',
    // Configure to create a single JS file and CSS file
    rollupOptions: {
      output: {
        // Force all JS into a single bundle
        manualChunks: () => 'app',
        // Configure CSS to be extracted to a single file
        assetFileNames: (assetInfo) => {
          if (assetInfo.name.endsWith('.css')) {
            return 'assets/styles.[hash].css';
          }
          return 'assets/[name].[hash].[ext]';
        },
      }
    },
    // Ensure CSS is extracted to its own file and not inlined
    cssCodeSplit: false
  },
  server: {
    port: 3000,
    proxy: {
      '/api': {
        target: 'http://localhost:5246',
        changeOrigin: true
      }
    }
  }
})