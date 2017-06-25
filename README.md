# BOCWelcomeLetterGenerator
This is a professional welcome generator for Bank of China (Australia) daily banking use. 
By simply entering customer details and account numbers, a PDF welcome letter is automatically generated in selected style.

# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## [Unreleased]
- Pre-set text editing function.
- Automatically log generation history in SQL database.
- Fetch account data for daily banking report using regular expression. And cross match it against generation history to identify possible error.

## [1.1.0] - 2017-06-26
### Fixed
- Minor Bug fixed for text display

## [1.1.0a] - 2017-06-25
### Added
- Setting panel: set default branch 
- Setting panel: set whether open file after generation by default
- Setting panel: set whether include email address in letter by default

### Changed
- Changed customer type selection panel to style selection: allow user to select welcome letter style to full fill need in multiple scenarios.
- Changed ways that pre-set text was stored. Prepare for user editing function in later version.

### Removed
- Removed branch selection panel at main tab. (as frequently switch branch is not necessary for daily banking use)

## [1.0.3] - 2017-06-20
### Added
- A branch selection combo box is added for multi-branch use.

## [1.0.2] - 2017-06-16
### Added
- A reset button is added for user to quickly clear all data on screen.

## [1.0.1] - 2017-06-13
### Added
- An improved account information validation method is involved. Now you can have more than 90% confidence for your input!

### Fixed
- A text display issue related to windows font has been solved
- A few typo corrections
- Bank information correction

## [1.0.0] - 2017-06-12
### First released version of C# app
- Automatically generate welcome letter in PDF
- Open file after generation
- Validate account information
- Support up to 4 accounts per customer
- Can optionally include customer address

## [0.1.0] - 2017-05-02
### An Excel version of app
- A temporary solution
