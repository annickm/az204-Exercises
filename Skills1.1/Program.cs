//dotnet core 2.2
using System;
using Microsoft.Azure.Management.Compute.Fluent.Models;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.Network.Fluent.Models;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;

namespace az204
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create the management client.This will be used for all the operations that we will perform in Azure.
            var credentials = SdkContext.AzureCredentialsFactory.FromFile("./azureauth.properties");
            var azure=Azure.Configure()
            .WithLogLevel(HttpLoggingDelegatingHandler.Level.Basic)
            .Authenticate(credentials)
            .WithDefaultSubscription();

            //First of all, we need to create a resource gorup where we will add all the resources needed for the virtual machine
            var groupName="az204-ResourceGroup";
            var vmName = "az204VMTesting";
            var location=Region.UKSouth;
            var vNetName="az204VNET";
            var vNetAddress = "172.16.0.0/16";
            var subnetName="az204Subnet";
            var subnetAddress = "172.16.0.0/24";
            var nicName="az204NIC";
            var adminUser="azadminuser";
            var adminPassword="Pa$$w0rd!2019!";
            var publicIPName="az204publicIP";
            var nsgName="az204VNET-NSG";

            Console.WriteLine($"Creating resourge group {groupName} ...");
            var resourceGroup = azure.ResourceGroups.Define(groupName).WithRegion(location).Create();

            //Every virtual machine needs to be connected to a virtual network.
            Console.WriteLine($"Creating virtual network {vNetName} ...");
            var network = azure.Networks.Define(vNetName)
                .WithRegion(location)
                .WithExistingResourceGroup(groupName)
                .WithAddressSpace(vNetAddress)
                .WithSubnet(subnetName, subnetAddress)
                .Create();

            //You need a public IP to be able to connect to the VM from the Internet
            Console.WriteLine($"Creating public IP {publicIPName} ...");
            var publicIP = azure.PublicIPAddresses.Define(publicIPName)
                .WithRegion(location)
                .WithExistingResourceGroup(groupName)
                .Create();

            //You need a network security group for controlling the access to the VM
            Console.WriteLine($"Creating Network Security Group {nsgName} ...");
            var nsg = azure.NetworkSecurityGroups.Define(nsgName)
                .WithRegion(location)
                .WithExistingResourceGroup(groupName)
                .Create();

            //You need a security rule for allowing access to the VM from the internet
            Console.WriteLine($"Creating a Security Rule for allowing remote access ...");
            nsg.Update()
                .DefineRule("Allow-RDP")
                    .AllowInbound()
                    .FromAnyAddress()
                    .FromAnyPort()
                    .ToAnyAddress()
                    .ToPort(3389)
                    .WithProtocol(SecurityRuleProtocol.Tcp)
                    .WithPriority(100)
                    .WithDescription("Allow-RDP")
                    .Attach()
            .Apply();

            //Any virtual machine need a network interface for connecting to the virtual network
            Console.WriteLine($"Creating virtual network interface {nicName} ...");
            var nic = azure.NetworkInterfaces.Define(nicName)
                .WithRegion(location)
                .WithExistingResourceGroup(groupName)
                .WithExistingPrimaryNetwork(network)
                .WithSubnet(subnetName)
                .WithPrimaryPrivateIPAddressDynamic()
                .WithExistingPrimaryPublicIPAddress(publicIP)
                .WithExistingNetworkSecurityGroup(nsg)
                .Create();
                
            //Create the virtual machine
            Console.WriteLine($"Creating virtual machine {vmName} ...");
            azure.VirtualMachines.Define(vmName)
                .WithRegion(location)
                .WithExistingResourceGroup(groupName)
                .WithExistingPrimaryNetworkInterface(nic)
                .WithLatestWindowsImage("MicrosoftWindowsServer", "WindowsServer", "2012-R2-Datacenter")
                .WithAdminUsername(adminUser)
                .WithAdminPassword(adminPassword)
                .WithComputerName(vmName)
                .WithSize(VirtualMachineSizeTypes.StandardDS2V2)
                .Create();


        }
    }
}
