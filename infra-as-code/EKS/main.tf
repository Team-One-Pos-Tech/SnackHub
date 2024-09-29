terraform {
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = ">= 5.61"
    }
  }
}

provider "aws" {
  region = local.region
  assume_role {
    role_arn = local.LabRoleArn
  }
}

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

  account_id = data.aws_caller_identity.current.account_id
  LabRoleArn = "arn:aws:iam::${local.account_id}:role/${var.LabRoleName}"
  PrincipalArn = "arn:aws:iam::${local.account_id}:role/${var.PrincipalRoleName}"
}

module "vpc" {
  source  = "terraform-aws-modules/vpc/aws"
  version = "5.13.0"

  name = local.name
  cidr = local.vpc_cidr

  azs             = local.azs
  private_subnets = [for k, v in local.azs : cidrsubnet(local.vpc_cidr, 4, k)]
  public_subnets  = [for k, v in local.azs : cidrsubnet(local.vpc_cidr, 8, k + 48)]
  intra_subnets   = [for k, v in local.azs : cidrsubnet(local.vpc_cidr, 8, k + 52)]

  enable_nat_gateway = true
  single_nat_gateway = true

  public_subnet_tags = {
    "kubernetes.io/role/elb" = 1
  }

  private_subnet_tags = {
    "kubernetes.io/role/internal-elb" = 1
  }

  tags = local.tags
}