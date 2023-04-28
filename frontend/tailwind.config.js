/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ['./src/**/*.{html,ts}'],
  theme: {
    extend: {
      colors: {
        background: {
          100: '#454545',
          200: '#3d3d3d',
          300: '#353535',
          400: '#2b2b2b',
          500: '#212121',
          600: '#171717',
          700: '#0d0d0d',
        },
      },
    },
    fontFamily: {
      sans: ['Inter', 'sans-serif'],
      serif: ['DM Serif Display', 'serif'],
    },
  },
  plugins: [],
};
