pipeline {
  agent any
  stages {
    stage('Import Assets') {
      steps {
        bat "$UNITY -batchmode -logFile - -projectPath $PROJECT -targetPlatform $PLATFORM -accept-apiupdate"
      }
    }

    stage('Run Unit Tests') {
      steps {
        sh "$UNITY -batchmode -logFile - -projectPath $PROJECT -targetPlatform $PLATFORM -runEditorTests"
      }
    }

    stage('Check Shaders') {
      steps {
        bat "$UNITY -batchmode -logFile - -projectPath $PROJECT -targetPlatform $PLATFORM -diag-debug-shader-compiler"
      }
    }

    stage('Build Player') {
      steps {
        sh "$UNITY -batchmode -logFile - -projectPath $PROJECT -targetPlatform $PLATFORM -buildWindows64Player $OUTPUT"
      }
    }

  }
  environment {
    UNITY = '"C:\\Program Files\\Unity\\Hub\\Editor\\2018.4.14f1\\Editor\\Unity.exe"'
    PROJECT = 'ContinuousIntegration'
    PLATFORM = 'StandaloneWindows64'
    OUTPUT = 'Build/ContinuousIntegration.exe'
  }
}