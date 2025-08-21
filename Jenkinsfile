pipeline {
    agent any

    environment {
        SOLUTION_NAME = "Incubation_Automation_API\\Incubation_Automation_API.sln"
        TEST_DLL = "D:\\Incubation_Automation_API\\Incubation_Automation_API\\bin\\Debug\\net8.0\\Incubation_Automation_API.dll"
        GIT_REPO = "https://github.com/Ajay655202/Incubation_API.git"
    }

    stages {
        stage('Checkout') {
            steps {
                git branch: 'master', url: "${GIT_REPO}"
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

stage('Build Solution') {
            steps {
                bat "\"C:\\Program Files\\Microsoft Visual Studio\\2022\\Community\\MSBuild\\Current\\Bin\\MSBuild.exe\" ${SOLUTION_NAME} /p:Configuration=Debug"
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
