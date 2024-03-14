resource "azurerm_linux_web_app" "this" {
  name                = var.azurerm_linux_web_app_name
  resource_group_name = data.azurerm_resource_group.this.name
  location            = azurerm_service_plan.this.location
  service_plan_id     = azurerm_service_plan.this.id


  app_settings = {
    ASPNETCORE_ENVIRONMENT = var.asp_environment
    WEBSITES_ENABLE_APP_SERVICE_STORAGE = "false"
    DOCKER_REGISTRY_SERVER_URL          = "https://${azurerm_container_registry.this.login_server}"
    DOCKER_REGISTRY_SERVER_USERNAME     = azurerm_container_registry.this.admin_username
    DOCKER_REGISTRY_SERVER_PASSWORD     = azurerm_container_registry.this.admin_password
    ConnectionStrings__DefaultDBConnection = "Host=${azurerm_postgresql_flexible_server.this.fqdn};Port=5432;Database=${azurerm_postgresql_flexible_server_database.this.name};Username=${azurerm_postgresql_flexible_server.this.administrator_login};Password=${azurerm_postgresql_flexible_server.this.administrator_password}"
  }


  site_config {
    application_stack {
      docker_image_name = "${var.oci_image_name}:${var.oci_image_tag}"
    }
    app_command_line = ""

  }

  
}

# Outputs
output "web_app_url" {
  value = azurerm_linux_web_app.this.default_hostname
}