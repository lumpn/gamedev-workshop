pipeline {
  agent any
  stages {
    stage('Import Assets') {
      steps {
        tool(name: 'Unity', type: 'Executable')
      }
    }

    stage('Run Unit Tests') {
      steps {
        tool(name: 'Unity', type: 'Executable')
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
}