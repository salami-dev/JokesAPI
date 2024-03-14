variable "azure_subscription_id" {
    description = "Sub Id"
  type        = string
  
}

variable "azure_tenant_id" {
  description = "Tenant ID"
  type        = string
}

variable "resource_group_name" {
    description = "Resourse Group Name"
  type        = string
}

variable "acr_name" {
  description = "Teenant ID"
  type        = string
}

variable "azurerm_service_plan_name" {
  type = string
  description = "Service pLan Name"
}

variable "azurerm_linux_web_app_name" {
  type = string
  description = "WebApp Name"
}

variable "acr_repo_name" {
  type = string
  description = "Azure Container Registry Repo Name"
}

variable "oci_image_name" {
  type = string
  description = "Name of Docker Image in Acr"
}

variable "oci_image_tag" {
  type = string
  description = "Image Tag"
}

variable "postgres_server_name" {
  type = string
  description = "DB Name on Azure"
}

variable "postgres_administrator_login" {
  type = string
  description = "PSQL username"
  default = "psqladmin"
}

variable "postgres_administrator_password" {
  type = string
  description = "PSQL Password"
}

variable "postgres_database_name" {
  type = string
  description = "(optional) describe your variable"
}

variable "asp_environment" {
  type = string
  description = "(optional) describe your variable"
}