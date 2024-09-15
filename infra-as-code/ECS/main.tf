terraform {

  required_providers {

    aws = {
      source  = "hashicorp/aws"
      version = "~> 5.0"
    }

  }

}

provider "aws" {

  region = "us-east-1"

}

resource "aws_vpc" "snack-hub-vpc" {

  cidr_block = "10.0.0.0/16"
  tags = {
    Name = "snack-hub-vpc"
  }

}

resource "aws_subnet" "snack-hub-subnet" {

  vpc_id     = aws_vpc.snack-hub-vpc.id
  cidr_block = "10.0.1.0/24"
  tags = {
    Name = "snack-hub-subnet"
  }

}

resource "aws_instance" "k8s-node" {

  ami           = "ami-0e86e20dae9224db8"
  instance_type = "t3.micro"
  count         = 5

  subnet_id = aws_subnet.snack-hub-subnet.id

  tags = {
    Name = "k8s-node-${count.index}"
  }

}
