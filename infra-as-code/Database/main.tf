locals {
  instance_size  = "M0"
  cloud_provider_free_tier = "TENANT"
  cloud_provider = "AWS"
  region         = "US_EAST_1"
}

resource "mongodbatlas_advanced_cluster" "cluster" {
  project_id     = mongodbatlas_project.snack_hub.id
  name           = var.cluster_name
  cluster_type   = "REPLICASET"
  backup_enabled = false

  replication_specs {
    region_configs {
      priority      = 7
      provider_name = local.cloud_provider_free_tier // To enable M0 [Free Tier]
      backing_provider_name = local.cloud_provider
      region_name   = local.region
      electable_specs {
        instance_size = local.instance_size
        node_count    = 3
      }
    }
  }
}

output "connection_strings" {
  value = mongodbatlas_advanced_cluster.cluster.connection_strings[0].standard_srv
}
