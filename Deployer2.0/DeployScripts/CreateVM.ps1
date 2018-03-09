param(
    [Parameter(Mandatory=$true,Position=1)]
    [string]$VMName,
    [Parameter(Mandatory=$true,Position=2)]
    [string]$DiskSize,
    [Parameter(Mandatory=$true,Position=3)]
    [string]$Memory,
    [Parameter(Mandatory=$true,Position=4)]
    [string]$CPUNum,
    [Parameter(Mandatory=$true,Position=5)]
    [string]$GuestId,
    [Parameter(Mandatory=$true,Position=6)]
    [string]$ISOLocation
   
)
cd "C:\Program Files (x86)\VMware\Infrastructure\PowerCLI\Scripts"
.\Initialize-PowerCLIEnvironment.ps1
#os ids
#ubuntu64Guest
#centos7Guest
#debian10Guest
#otherLinuxGuest
#fedoraGuest
function getISOImage($OSId){
    switch ($OSId) {
        ($OSId -eq "ubuntu64Guest") { $result = "[VSANDatastore] ISO Images/ubuntu-16.04.3-server-amd64.iso" }
        ($OSId -eq "debian10Guest") { $result = "[VSANDatastore] ISO Images/kali-linux-2016.1-amd64.iso" }
        ($OSId -eq "otherLinuxGuest") { $result = "[VSANDatastore] ISO Images/archlinux-2018.03.01-x86_64.iso" }
        ($OSId -eq "centos7Guest") { $result = "[VSANDatastore] ISO Images/CentOS-7x86_64-Everything-1511 (1).iso" }
        ($OSId -eq "fedoraGuest") { $result = "[VSANDatastore] ISO Images/Fedora-Workstation-Live-x86_64-25-1.3.iso" }
    }
    return $result
}
Connect-VIServer -Server 10.0.88.11 -User "administrator@vsphere.local" -Password Nu140859246!
$cluster = "VSANCluster"
$resourcePool = (Get-Datacenter "Datacenter" | Get-Cluster -Name $cluster | Get-Resourcepool -name Resources)
New-VM -Name $VMName -Datastore 'vsanDatastore' -DiskGB $DiskSize -MemoryGB $Memory -NumCpu $CPUNum -NetworkName "Internal (DPG)" -ResourcePool $resourcePool -GuestId $GuestId
$cd = New-CDDrive -VM $VMName -IsoPath $ISOLocation
Set-CDDrive -CD $cd -StartConnected: $true -Confirm: $false
Start-Sleep -Seconds 60
Start-VM -VM $VMName -Confirm $false
