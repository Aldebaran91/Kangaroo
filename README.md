Kangaroo - Library
======================

## Description
The Kangaroo library is more than just a queue. It collects all 
data (exceptions, sensor data, ...) that should be collected. For 
each type a seperate Kangaroo instance is created. Each instance 
has one or more export manager implementations (developed by user). 
These export manager can be extended by a converter (also developed 
by user). At the end it should standardize the collecting and exporting 
type. Updates on each export/converter implementations should have 
no impact in already implemented projects.

## Installation
Add it to your .Net Framework or DotNet project. The library 
Kangaroo supports .Net Standard 1.0.

## Contribution
Create a fork of the project into your own reposity. Make 
all your necessary changes and create a pull request with a 
description on what was added or removed and details explaining 
the changes in lines of code. If approved, project 
owners will merge it.

Licensing
---------
This project is under MIT-Licence (see LICENSE file).

Support
-------
Please file bugs and issues on the Github issues page for this 
project. This is to help keep track and document everything 
related to this repo. The code and documentation are released 
with no warranties or SLAs and are intended to be supported 
through a community driven process.
