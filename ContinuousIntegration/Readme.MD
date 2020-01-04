# Continuous Integration

## Goal
Catch bugs early.

## Method
Verify project automatically.

## Prerequesites
- Version control
- Jenkins
- Automated tests
  - Unit test
  - Asset test
  - Integration test
- Build server
- PostProcessorBuild attribute

## Other
- SteamPipe
- F8 feedback
- Analytics
- Sentry

## Details
Manage Jenkins -> Global Tool Configuration -> Git
https://stackoverflow.com/questions/51500698/where-to-find-option-to-change-workspace-path-location-in-jenkins
Actually has to modify config.xml in the Jenkins install dir. See workspaceDir element.
Better yet, add a local agent. Start like Launch agent via execution of command on the master c:\Program Files (x86)\Jenkins\jre\bin\java -jar W:\Jenkins\agent.jar

## Notes
1. Install Jenkins from .msi (doesn't ask any options)
2. Open localhost:8080
3. Unlock with initial admin password (follow instructions on screen)
4. Select plugins to install (more later)
5. Select none (top), select GitHub, GitHub Authentication, Pipeline, install.
6. Skip creating first admin user. Continue as admin (bottom).
7. Accept default instance configuration (http://localhost:8080, no https!). Save and finish.
8. Manage Jenkins -> Configure System -> Usage: ony build jobs with label expressions matching this node

9. Manage Jenkins -> Global Security -> Agents -> TCP port for inbound agents: random
10. Manage Jenkins -> Manage Nodes -> New Node (name Agent1, permanent agent, keep defaults)
11. Manage Jenkins -> Manage Nodes -> Agent1 -> agent.jar save file.
12. Create batch file "c:\Program Files (x86)\Jenkins\jre\bin\java" -jar agent.jar -jnlpUrl http://localhost:8080/computer/Agent1/slave-agent.jnlp -secret 7a729911603e5918b83052a24e7258dc26ad85359831d6e040d0999f5de5eb8b 
13. Install as service...

14. Manage Jenkins -> Global Tool Configuration -> Git -> Delete -> Add Git -> JGit
15. Add jobs...


## See also
[Unity Cloud Build](https://unity3d.com/unity/features/cloud-build) does not work with consoles.

## References

https://jenkins.io/blog/2017/04/05/welcome-to-blue-ocean/
https://jenkins.io/blog/2017/04/06/welcome-to-blue-ocean-editor/
https://jenkins.io/blog/2017/04/11/welcome-to-blue-ocean-pipeline-activity/
https://jenkins.io/blog/2017/04/12/welcome-to-blue-ocean-dashboard/

- [Unite 2015 - Continuous Integration with Unity](https://www.youtube.com/watch?v=kSXomLkMR68)
- [Continuous integration and automated testing](http://itmattersgames.com/2019/02/18/continuous-integration-and-automated-testing/)
- [Unity Build Automation with Jenkins](https://smashriot.com/unity-build-automation-with-jenkins/)
- [Jenkins for Unity](https://github.com/CarlHalstead/Jenkins-for-Unity)
- [Unity build automation with Jenkins](https://benhoffman.tech/general/2018/07/12/unity-build-automation-with-jenkins.html)


- [Setting Up a Build Server for Unity with Jenkins](https://www.youtube.com/watch?v=4J3SmhGxO1Y)
