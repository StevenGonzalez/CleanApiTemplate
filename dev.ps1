param(
    [Parameter(Position = 0)]
    [ValidateSet('up', 'down', 'logs', 'migrate', 'test')]
    [string]$Command = 'up'
)

switch ($Command) {
    'up' {
        docker compose up -d --build
    }
    'down' {
        docker compose down
    }
    'logs' {
        docker compose logs -f api
    }
    'migrate' {
        dotnet ef database update --project .\DotnetCleanApiTemplate\DotnetCleanApiTemplate.csproj --startup-project .\DotnetCleanApiTemplate\DotnetCleanApiTemplate.csproj
    }
    'test' {
        dotnet test DotnetCleanApiTemplate.sln
    }
}
