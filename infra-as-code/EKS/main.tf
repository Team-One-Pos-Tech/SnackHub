locals {
  name   = "snack-hub"
  region = "us-east-1"

  vpc_cidr = "10.0.0.0/16"
  azs      = slice(data.aws_availability_zones.available.names, 0, 3)

  tags = {
    Example    = local.name
    GithubRepo = "https://github.com/Team-One-Pos-Tech/SnackHub"
    GithubOrg  = "https://github.com/Team-One-Pos-Tech"
  }
}

resource "aws_eks_cluster" "snack_hub" {
  name     = "${local.name}-cluster"
  role_arn = "arn:aws:iam::${var.account_id}:role/LabRole"
  version  = 1.31

  vpc_config {
    subnet_ids              = module.vpc.private_subnets
    endpoint_private_access = true
    endpoint_public_access  = true

  }

  access_config {
    authentication_mode = var.accessConfig
  }

  tags = local.tags
}