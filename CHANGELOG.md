# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## [1.1.0] / 2023-11-19
### Features
- Add `net6.0-windows` support
### Changed
- Remove `ObservableCollection`, use `System.Collections.ObjectModel.ObservableCollection` instead.

## [1.0.5] / 2022-07-26
### Fixed
- Add `ObservableObject` in `AsyncRelayCommand` to update `IsExecuting` 
### Changed
- Change `ExceptionHandler` to `IRelayCommand`

## [1.0.4] / 2022-07-26
### Features
- Add `SetExceptionHandler` in all IRelayCommand
### Changed
- Changed to `net45`
- Changed App Icon
- Changed `RaiseCanExecuteChanged` to private
### Fixed
- Fix `RaiseCanExecuteChanged` not trigger `Exception`

## [1.0.3] / 2022-06-30
### Added
- Add `AsyncRelayCommand` and `AsyncRelayCommand{T}`
- Add Task Extension
### Fixed
- Fix `RelayCommand{T}` CanExecute

## [1.0.2] / 2022-06-20
### Updated
- Update/Fix `RelayCommand{T}` CanExecute

## [1.0.1] / 2022-06-06
### Added
- Add ObservableCollection

## [1.0.0] / 2022-03-24
### Changed
- Move to Nuget Package
### Example
- Add Resource
- Add Panel/Button/Icon
- Add MainViewModel
- Add MainView
- Add MainModel
### Added
- ObservableObject with `INotifyPropertyChanged`
- RelayCommand / RelayCommand(T)

[vNext]: ../../compare/1.0.0...HEAD
[1.1.0]: ../../compare/1.0.5...1.1.0
[1.0.5]: ../../compare/1.0.4...1.0.5
[1.0.4]: ../../compare/1.0.3...1.0.4
[1.0.3]: ../../compare/1.0.2...1.0.3
[1.0.2]: ../../compare/1.0.1...1.0.2
[1.0.1]: ../../compare/1.0.0...1.0.1
[1.0.0]: ../../compare/1.0.0