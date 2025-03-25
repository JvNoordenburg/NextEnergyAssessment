provider "azurerm" {
  
}

resource "azurerm_resource_group" "resource_group" {
  name = "next-energy-eu-west"
  location = "West Europe"
}

resource "azurerm_postgresql_database" "db" {
  name = "imbalance-db"
  resource_group_name = azurerm_resource_group.resource_group.name
  location = azurerm_resource_group.resource_group.location

  lifecycle {
     prevent_destroy = true
  }
}

# Add service bus & queues

# Add function app for cron job with timed trigger & appsettings config
# Add function app with queue trigger for imbalance processor
# Add function app with queue trigger & autoscaling for battery processor (including max scaling parameters)