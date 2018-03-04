param(
    [Parameter(Mandatory=$true,Position=1)]
    [string]$containerName,
    [Parameter(Mandatory=$true,Position=2)]
    [string]$containerTemplate
)
function login ([string[]]$commands) {
   $commands | %{
    plink -ssh "Container Environment" -l root -pw Pokemon18 $_ 
   }
}
$testArray = 
    "lxc-create -n $containerName -t $containerTemplate",
    "lxc-start -n $containerName -d"
login($testArray)


 

# if (plink -ssh -shareexists) {
    
# }