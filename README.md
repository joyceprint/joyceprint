![JoycePrint](./readme-images/jp-logo-rbg.png)

# Overview

The joyceprint website, built in MVC 5 and .NET 4.6.2, hosted on IIS 8.5
The site is a simple application that provides basic information and a simple form that allows the user to submit information for a job quote.

## Projects

The following common projects contain the shared functionality for their operations

- **Common.MVC**
  - Common helper functions and attributes used by the mvc websites.
- **Common.Providers**
  - A common base project used to instantiate service providers.
  - This is used by the logging and analytics projects.
- **Common.Security**
  - The security cipher used by the application
- **Utility.Security**
  - The command line tool for encrypting and decrypting sensitive information

The following projects handle analytics

- **Common.Analytics**
  - A common base analytics provider project, used to capture page hits and events
- **Common.Analytics.GoogleAnalytics**
  - The implementation of a analytics provider for the google analytics platform

The following projects handle application logging

- **Common.Logging**
  - A common base logging provider project, used to log application messages
- **Common.Logging.ElmahLogger**
  - The implementation of a logging provider for Elmah, which persists to a mysql database
- **Common.Logging.FileLogger**
  - The implementation of a logging provider for a flat file
- **Common.Logging.WindowsEventLogger**
  - The implementation of a logging provider for the windows event log

The following proects handle the joyceprint site

- JoycePrint.Docs
- JoycePrint.Domain
- JoycePrint.Web
- JoycePrint.Domain.Tests
- JoycePrint.Web.Tests

The following projects handle the docketbooks site

- DockerBooks.Web
- DockerBooks.Web.Tests

## SSL

To add SSL to the website for free we use [SSL For Free](https://www.sslforfree.com/)
Following the guide [here](https://wallydavid.com/visual-guide-installing-lets-encrypt-ssl-media-temple-or-a-plesk-hosting-account/)

### Setup

Create a folder at the root of the website called `.well-known` with the permission set `755`

Add a web config file to the `.well-known` hosts with the following contents

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <system.webServer>
    <!-- This will stop any redirects you have at the higher level -->
    <httpRedirect enabled="false" />

    <!-- This will stop any integrated mode settings you have at the higher level -->
    <validation validateIntegratedModeConfiguration="false"/>
  </system.webServer>

  <!-- This will allow unauthenticated users to acme-challenge subfolder -->
  <location path="acme-challenge">
    <system.web>
      <authorization>
        <allow users="*"/>
      </authorization>
    </system.web>
  </location>
</configuration>
```

Create a folder inside the `well-known` folder called `acme-challenge` with the permission set `755`

Add a web config file to the `acme-challenge` hosts with the following contents

```xml
<?xml version="1.0" encoding="UTF-8"?>
<configuration>
    <system.webServer>
        <staticContent>
            <mimeMap fileExtension="." mimeType="text/plain" />
        </staticContent>
    </system.webServer>
</configuration>
```

Add the files that `SSL For Free` provides to the `acme-challenge` with the permission set `644`
