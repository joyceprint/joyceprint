![JoycePrint](./readme-images/jp-logo-rbg.png)

# Overview

The joyceprint website, built in MVC 5 and .NET 4.6.2, hosted on IIS 8.5
The site is a simple application that provides basic information and a simple form that allows the user to submit information for a job quote.

## Projects

### Common Projects

The following projects contain the shared functionality for their operations

#### Common.MVC

Common helper functions and attributes used by the mvc websites.

#### Common.Providers

A common base project used to instantiate service providers.
This is used by the logging and analytics projects.

#### Common.Security

The security cipher used by the application

#### Utility.Security

The command line tool for encrypting and decrypting sensitive information

### Analytics Projects

The following projects handle analytics

#### Common.Analytics

A common base analytics provider project, used to capture page hits and events

#### Common.Analytics.GoogleAnalytics

The implementation of a analytics provider for the google analytics platform

### Logging Projects

The following projects handle application logging

#### Common.Logging

A common base logging provider project, used to log application messages

#### Common.Logging.ElmahLogger

The implementation of a logging provider for Elmah, which persists to a mysql database

#### Common.Logging.FileLogger

The implementation of a logging provider for a flat file

#### Common.Logging.WindowsEventLogger

The implementation of a logging provider for the windows event log

### JoycePrint Site

JoycePrint.Docs
JoycePrint.Domain
JoycePrint.Web
JoycePrint.Domain.Tests
JoycePrint.Web.Tests

### DocketBooks Site

DockerBooks.Web
DockerBooks.Web.Tests
