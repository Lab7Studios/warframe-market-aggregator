[![Build status](https://ci.appveyor.com/api/projects/status/gu1pvk7ipw3b9e1t/branch/master?svg=true)](https://ci.appveyor.com/project/rehret/warframe-market-aggregator/branch/master)

# Warframe Market Aggregator
Service to fetch and cache data from [Warframe Market](http://warframe.market).

## Getting Started
#### Prerequisites
[.NET Core](https://dotnet.github.io/) is required. Follow the Getting Started guide on that page to install the .NET Core runtime.

If using any OS other than Window, edit docker-compose.yml and change the log path from `C:\\logs` to something more appropriate, such as `/logs`.

#### Running
```bash
dotnet run
```

## Contributing
Install [EditorConfig](http://editorconfig.org/) in your editor. This will help keep a consistent code style throughout the project.

This project makes use of [GitHub Flow](https://guides.github.com/introduction/flow/). As such, work should be done on a feature branch and a pull request opened against `master` once the work is complete. Feature branches should following the naming convention `feature/<feature-name>`.

#### Versioning
Version numbers will follow [SemVer](https://semver.org/) versioning:
> Major.Minor.Patch

For example, this is a valid version number:
> 1.2.345

Any changes that affect the major, minor, or patch versioning should have a tag pushed to `origin` with the SemVer version (consisting of major, minor, and patch).
For example, if the project was at version `0.1.0` and it was decided that it was ready for official release, the tag `1.0.0` would be pushed to origin at that commit.

## License
[MIT](LICENCE)
