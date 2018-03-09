cd "C:\Program Files (x86)\VMware\Infrastructure\PowerCLI\Scripts"
.\Initialize-PowerCLIEnvironment.ps1
Connect-VIServer -Server 10.0.88.11 -User "administrator@vsphere.local" -Password Nu140859246!
Get-VM 