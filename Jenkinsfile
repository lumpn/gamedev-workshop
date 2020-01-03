pipeline {
  agent any
  stages {
    stage('Import Assets') {
      steps {
        bat '${env.UNITY} -batchmode -projectPath ProjectPath -targetPlatform TARGET_PLATFORM -accept-apiupdate '
      }
    }

    stage('Run Unit Tests') {
      steps {
        sh 'Unity.exe -batchmode -projectPath ProjectPath -targetPlatform TARGET_PLATFORM -runEditorTests'
      }
    }

    stage('Check Shaders') {
      steps {
        bat 'Unity -batchmode -projectPath ProjectPath -targetPlatform TARGET_PLATFORM -diag-debug-shader-compiler'
      }
    }

    stage('Build Player') {
      steps {
        sh 'Unity.exe -batchmode -projectPath ProjectPath -targetPlatform TARGET_PLATFORM -buildWindows64Player TargetPath/File.exe'
      }
    }

  }
  environment {
    UNITY = 'C:\\Program Files\\Unity\\Hub\\Editor\\2018.4.14f1\\Editor\\Unity.exe'
  }
}