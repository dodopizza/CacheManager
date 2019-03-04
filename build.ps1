Param(
    [string] $Branch = "master",
    [string] $Build = "000000"
)

Function CreateNugetConfig {
	$defaultNugetConfig = "<configuration>
	<packageSources>
		<add key=`"nuget.org`" value=`"https://api.nuget.org/v3/index.json`" protocolVersion=`"3`" />
		<add key=`"mygetdodo`" value=`"https://www.myget.org/F/dodopizza/api/v3/index.json`" />
	</packageSources>
	<packageSourceCredentials>
		<mygetdodo>
			<add key=`"Username`" value=`"$env:NUGET_USER`" />
			<add key=`"ClearTextPassword`" value=`"$env:NUGET_PASSWORD`" />
		</mygetdodo>
	</packageSourceCredentials>
</configuration>"
	Write-Output $defaultNugetConfig > NuGet.config
}

Function GetAuthHeaders() 
{
	$pair = "${env:NUGET_USER}:${env:NUGET_PASSWORD}"
	$encodedCreds = [System.Convert]::ToBase64String([System.Text.Encoding]::ASCII.GetBytes($pair))
	$basicAuthValue = "Basic $encodedCreds"
	return @{
		Authorization = $basicAuthValue
	}
}

Function IsPackagePublished($headers, $name, $version) 
{
	$exists = $FALSE
	try {
		$uri = "https://www.myget.org/F/dodopizza/api/v3/registration1/${name}/${version}.json"
		if ((Invoke-WebRequest -Uri $uri -Headers $headers).StatusCode -eq 200) {
			$exists = $TRUE
		}
	}
	catch {
		$exists = $FALSE
	}
	return $exists
}

Function PublishPackages($folder)
{
    $version = "2.0.0-$env:VersionSuffix-$env:BuildNumber.nupkg"
    $headers = GetAuthHeaders
    $source = "https://www.myget.org/F/dodopizza/"

    Get-ChildItem -Path ./artefacts/ -Filter *.nupkg | ForEach-Object -Process {
        $fileName = $_.Name
        $packageName = $_.Name.Replace($version, "").Trim('.')
        
        Write-Host "Trying to push package '$packageName' with version '$version' located at file with name '$fileName'"
        $exists = IsPackagePublished -headers $headers -name $packageName -version $version
        If ($exists)
        {
            Write-Host "Package already exists, skipping"
        }
        Else 
        {
            Write-Host "Package not exists, pushing to feed '$source'"
            dotnet nuget push $_.FullName --source $source
        }
    }
}

if (!(Test-Path -Path "NuGet.config"))
{
    CreateNugetConfig 
}

Write-Host "Restoring all packages for solution"
dotnet restore 

Write-Host "Building packages for all solution"
dotnet build --no-restore --configuration Release 

$env:VersionSuffix="beta"
$env:BuildNumber="$Branch-$Build"
Write-Host "Setting version suffix to $env:VersionSuffix and build number to $env:BuildNumber"

New-Item -ItemType directory -Path ./artefacts -Force
Write-Host "Packaging artefacts to ./artefacts folder"

dotnet pack --output $pwd/artefacts --no-restore --configuration Release src/CacheManager.Core 
dotnet pack --output $pwd/artefacts --no-restore --configuration Release src/CacheManager.Couchbase 
dotnet pack --output $pwd/artefacts --no-restore --configuration Release src/CacheManager.Microsoft.Extensions.Caching.Memory 
dotnet pack --output $pwd/artefacts --no-restore --configuration Release src/CacheManager.Microsoft.Extensions.Configuration 
dotnet pack --output $pwd/artefacts --no-restore --configuration Release src/CacheManager.Microsoft.Extensions.Logging 
dotnet pack --output $pwd/artefacts --no-restore --configuration Release src/CacheManager.StackExchange.Redis 
dotnet pack --output $pwd/artefacts --no-restore --configuration Release src/CacheManager.SystemRuntimeCaching 
dotnet pack --output $pwd/artefacts --no-restore --configuration Release src/CacheManager.Serialization.Bond
dotnet pack --output $pwd/artefacts --no-restore --configuration Release src/CacheManager.Serialization.DataContract
dotnet pack --output $pwd/artefacts --no-restore --configuration Release src/CacheManager.Serialization.Json 
dotnet pack --output $pwd/artefacts --no-restore --configuration Release src/CacheManager.Serialization.ProtoBuf 

Write-Host "Pushing artefacts to remote registry"
PublishPackages -folder ./artefacts