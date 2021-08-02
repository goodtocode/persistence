####################################################################################
Set-ExecutionPolicy Unrestricted -Scope Process -Force
$VerbosePreference = 'SilentlyContinue' # 'Continue'
[String]$ThisScript = $MyInvocation.MyCommand.Path
[String]$ThisDir = Split-Path $ThisScript
Set-Location $ThisDir # Ensure our location is correct, so we can use relative paths
Write-Host "*****************************"
Write-Host "*** Starting: $ThisScript On: $(Get-Date)"
Write-Host "*****************************"
####################################################################################
# Init
function Get-AuthToken
{
     param
         (
         [Parameter(Mandatory=$true)]
         $TenantName='goodtocode.com'
         )
     Import-Module Azure
     $clientId = "653f20c0-d49b-48f3-a471-1ad0173fc6aa" 
     $redirectUri = "urn:ietf:wg:oauth:2.0:oob"
     $resourceAppIdURI = "https://graph.microsoft.com"
     $authority = "https://login.microsoftonline.com/$TenantName"
     $authContext = New-Object "Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationContext" -ArgumentList $authority
    $Credential = Get-Credential
     $AADCredential = New-Object   "Microsoft.IdentityModel.Clients.ActiveDirectory.UserCredential" -ArgumentList   $credential.UserName,$credential.Password
     $authResult = $authContext.AcquireToken($resourceAppIdURI, $clientId,$AADCredential)
     return $authResult

 }
 Get-AuthToken -TenantName "goodtocode.com"