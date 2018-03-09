param(
    [Parameter(Mandatory=$true,Position=1)]
    [string]$VMName
)
cd "C:\Program Files (x86)\VMware\Infrastructure\PowerCLI\Scripts"
.\Initialize-PowerCLIEnvironment.ps1
Connect-VIServer -Server 10.0.88.11 -User "administrator@vsphere.local" -Password Nu140859246!
$resourcePool = (Get-Datacenter "Datacenter" | Get-Cluster -Name "VSANCluster" | Get-Resourcepool -name Resources)
New-VM -Name psCloneDB -VM "DatabaseModel" -ResourcePool $resourcePool
Start-VM -VM $VMName -Confirm $false
Invoke-VMScript -VM $VMName -ScriptText configurationScript.sh -GuestUser ssduser -GuestPassword letmein123 -ScriptType Bash
