# error-tester
Sample application for how to surface errors into New Relic

Provided Docker container includes New Relic .NET Agent for .NET Core Applications

Prerequisites:
- Docker (https://www.docker.com/products/docker-desktop)
- Docker-Compose (Included with Docker Desktop for Windows and MacOS.  Seperate install for Linux environments)
- NewRelic License Code

Installation
1. Rename newrelic.config.example to newrelic.config
2. Open newrelic.config file in text editor.  Replace `YOUR_KEY_NAME` with the NewRelic license code.  Replace `YOUR_APP_NAME` with the name of your application as it will appear in the New Relic window.
3. On command prompt in the root directory of this repository run `docker-compose up` to start the application.
