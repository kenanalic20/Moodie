@echo off
echo Starting Moodie application...

REM Check if frontend/node_modules exists
IF NOT EXIST frontend\node_modules (
    echo Frontend node_modules not found. Installing dependencies...
    
    REM Check if pnpm is available
    where pnpm >nul 2>nul
    IF %ERRORLEVEL% EQU 0 (
        echo Using pnpm to install dependencies...
        cd frontend
        call pnpm install
        cd ..
    ) ELSE (
        echo pnpm not found, using npm instead...
        cd frontend
        call npm install
        cd ..
    )
)

REM Create necessary directories for backend
echo Checking backend directories...
IF NOT EXIST backend\Uploads mkdir backend\Uploads
IF NOT EXIST backend\Uploads\Images mkdir backend\Uploads\Images
echo Backend directories verified.

REM Start backend
echo Starting backend...
start cmd /k "cd backend && dotnet watch run"

REM Start frontend
echo Starting frontend...
cd frontend
where pnpm >nul 2>nul
IF %ERRORLEVEL% EQU 0 (
    start cmd /k "pnpm start"
) ELSE (
    start cmd /k "npm start"
)

echo Moodie application is starting up...
