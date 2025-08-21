pipeline {
    agent any

    environment {
        SOLUTION_NAME = "Automation_Incubation\\Automation_Incubation.sln"
        TEST_DLL = "D:\\Automation_Incubation_Repo\\Automation_Incubation\\bin\\Debug\\net8.0\\Automation_Incubation.dll"
        GIT_REPO = "https://github.com/Ajay655202/Automation_Incubation.git"
    }

    stages {
        stage('Checkout') {
            steps {
                git branch: 'main', url: "${GIT_REPO}"
            }
        }
		
		 stage('Restore') {
            steps {
                bat "dotnet restore ${SOLUTION_NAME} --no-cache --force"
            }
        }
        stage('Build') {
            steps {
                bat "dotnet build ${SOLUTION_NAME} --configuration Release"
            }
        }
        stage('Test') {
            steps {
                bat "dotnet test ${SOLUTION_NAME} --configuration Release"
            }
        }

        stage('Restore Packages') {
            steps {
                bat "nuget restore ${SOLUTION_NAME}"
            }
        }       

        stage('Run Regression Tests') {
            steps {
                bat "\"C:\\Program Files\\Microsoft Visual Studio\\2022\\Community\\Common7\\IDE\\CommonExtensions\\Microsoft\\TestWindow\\vstest.console.exe\" ${TEST_DLL} --TestCaseFilter:TestCategory=Regression /logger:trx"
            }
        }

        stage('Publish Results') {
            steps {
                mstest testResultsFile:"**/*.trx"
            }
        }
    }
}
