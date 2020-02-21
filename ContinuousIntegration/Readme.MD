# Continuous Integration
Keep testing. Keep shipping. Catch bugs early.

## Problem
Game assets tend to have lots of dependencies across various disciplines. Projects can easily break when people change things without realizing which other things depend on them. The longer it takes to notice the problem, the longer it takes to identify and fix the change that caused it.

Furthermore, at any given time there's a slightly different work-in-progress version of the project on each developer's machine, making it difficult to reproduce issues seen on another machine. Some issues might even manifest on the target platform only.

```
¯\_(ツ)_/¯
It works on
my machine.
```

## Solution
We want to reduce the time between making a change and seeing it on the target platform as much as possible. We want to have checks that tell us when something broke. We want to run those checks after every change. That means we have to automate the entire process.

We achive continuous integration and delivery by
1. maintaining a single repository,
2. automating the build process,
3. automating the testing process, and
4. automating the shipping process.

## Maintaining a single repository
If you're already using [version control](https://en.wikipedia.org/wiki/Version_control) you can skip ahead. Keep using what works for you. If you're not using version control yet, currently your best options are [GitHub Enterprise](https://github.com/enterprise) and [Plastic SCM](https://www.plasticscm.com/). Both [cost](https://github.com/pricing) [money](https://www.plasticscm.com/pricing), but not too much. They are well worth it. Stay away from [Unity Collaborate](https://unity.com/unity/features/collaborate). It doesn't work properly and its feature set is severly limited. It might be useful for game jams, but that's about it.

## Automating the build process
If you're using GitHub Enterprise, you might want to give [GitHub Actions](https://github.com/features/actions) a try. [Unity Cloud Build](https://unity3d.com/unity/features/cloud-build) is too basic for anything serious. We'll be using [Jenkins](https://jenkins.io/) here, which is free, open source, and runs locally.

### Jenkins
We connect Jenkins to the repository and tell it to build the project every time a change gets committed. That's a lot of builds, so it's best to [install Jenkins](https://jenkins.io/download/thank-you-downloading-windows-installer-stable/) on an unused machine, which we will use as our build server.

After installing Jenkins and logging in to http://localhost:8080 with the initial admin password, Jenkins will ask which plugins to install. We're going to "Install suggested plugins". We won't need most of them, but it's too tedious to pick plugins manually. Speaking of plugins, everything in Jenkins is a plugin, most of them contributed by volunteers. Most of them are not *production ready*, which is why you will often find several plugins for the same task. We're going to bypass all that keep things as simple as possible.

### Build job
To make Jenkins build the game, we need to wrap the build process in a Jenkins job, so let's create a new job using the "Pipeline" template.

![Jenkins welcome page](Documentation/Jenkins1.png "It works!")
![Jenkins create job page](Documentation/Jenkins2.png "Pipelines for Mario")
![Jenkins pipeline setup](Documentation/Pipeline1.png)
![Jenkins pipeline triggers](Documentation/Pipeline2.png)
![Jenkins pipeline script](Documentation/Pipeline3.png)

The job configuration in Jenkins is fairly minimal. All we need is for Jenkins to download the [pipeline script](https://jenkins.io/doc/book/pipeline/jenkinsfile/) from our version control system and then execute it.

In [our pipeline script](BuildScripts/Jenkins/Jenkinsfile), we
1. update to the latest revision on the repository,
2. start Unity and let it import assets,
3. run the unit tests,
4. make a build, and finally
5. upload the build.

```groovy
    stage('Import Assets') {
      steps {
        bat "$UNITY -batchmode -logFile - -projectPath $PROJECT -buildTarget $PLATFORM -quit -accept-apiupdate"
      }
    }
    stage('Run Unit Tests') {
      steps {
        bat "$UNITY -batchmode -logFile - -projectPath $PROJECT -buildTarget $PLATFORM -runEditorTests"
      }
    }
    stage('Build') {
      steps {
        bat "$UNITY -batchmode -logFile - -projectPath $PROJECT -buildTarget $PLATFORM -quit -buildWindows64Player $OUTPUT"
      }
    }
```

## Automatic the testing process
We're already running the unit tests before each build. It is a good idea to cover as many potential problems as possible using unit tests. We start with a basic set of consistency checks.

```csharp
        [Test]
        public void ShaderHasErrors()
        {
            var infos = ShaderUtil.GetAllShaderInfo();
            foreach (var info in infos)
            {
                Assert.IsFalse(info.hasErrors, "Shader '{0}' has errors.", info.name);
            }
        }
```

It takes time to develop a comprehensive test suite. As a guideline, it's a good idea to add a test each time a new issue appears in the build. The goal is to write the new test in such a way that it prevents the issue from ever happening again.

## Automating the shipping process
We now have a build that passed basic checks, but that still doesn't mean much. What we need is *real people* testing it.

```
It does not exist until you ship it. --Jonas Bötel
```

The good news is that [uploading builds to Steam](https://partner.steamgames.com/doc/sdk/uploading) is reasonably straightforward and well documented.

```groovy
    stage('Upload Build') {
      steps {
        echo "$STEAMCMD +login $STEAMUSERNAME $STEAMPASSWORD +run_app_build $STEAMSCRIPT"
      }
    }
```

Steam keeps all uploaded builds around *forever*, which is super useful when something goes wrong and we have to revert to an earlier build. Let's make sure to upload our builds to a beta branch on Steam though, by setting `setlive` in the `app_build.vdf`. That way our players can opt in to play the latest ~~and greatest~~ builds if they feel brave. Every few weeks, when the build is stable enough, we can promote it to the default branch in Steam's backend.

## Further reading
- [Continuous Integration](https://martinfowler.com/articles/continuousIntegration.html) by Martin Fowler
- [Continuous integration and automated testing](http://itmattersgames.com/2019/02/18/continuous-integration-and-automated-testing/) by Michele Krüger
- [Unite 2015 - Continuous Integration with Unity](https://www.youtube.com/watch?v=kSXomLkMR68) by Jonathan Peppers
- [Setting Up a Build Server for Unity with Jenkins](https://www.youtube.com/watch?v=4J3SmhGxO1Y) by William Chyr

## Translations
If you find this workshop useful and speak another language, I'd very much appreciate any help translating the chapters. Clone the repository, add a localized copy of the README.md, for example README-pt-BR.md, and send me a pull request.
