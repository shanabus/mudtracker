# Mud Tracker 1.0
This Hosted Blazor application determines if you'll need boots to trudge throught the mud in 3 days
- likelihood of rain
- temp is above freezing

This version also includes a probability for mud based on presence of rain in 2 days along with humidity and wind conditions.

Also includes support for determining your location through JSInterop and the browsers Geolocation API.

![tests](https://github.com/shanabus/mudtracker/actions/workflows/dotnet.yml/badge.svg) ![deploy](https://github.com/shanabus/mudtracker/actions/workflows/deploy.yml/badge.svg)

# Build docker image
Clone or download repo and from the **\MudTracker** folder, build the image

```bash
docker build -t mudtracker .
```

# Run docker
Be sure to provide your own API KEY for the OpenWeatherApi to work

```bash
docker run -d -e Weather__ApiKey=YOUR_API_KEY_HERE -p 80:80 mudtracker:latest --rm
```

# Tests
Execute the tests from the **MudTracker** folder with the following command

```bash
dotnet test .\Tests\MudTracker.Tests.csproj
```

Additional parameters for testing [here](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-test)
