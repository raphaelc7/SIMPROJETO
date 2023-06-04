-- --------------------------------------------------------
-- Servidor:                     127.0.0.1
-- Versão do servidor:           5.7.40-log - MySQL Community Server (GPL)
-- OS do Servidor:               Win64
-- HeidiSQL Versão:              12.1.0.6537
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Copiando estrutura do banco de dados para simprojeto
CREATE DATABASE IF NOT EXISTS `simprojeto` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `simprojeto`;

-- Copiando estrutura para tabela simprojeto.clientes
CREATE TABLE IF NOT EXISTS `clientes` (
  `Codigo` int(11) NOT NULL AUTO_INCREMENT,
  `S_Nome` varchar(120) DEFAULT NULL,
  `S_Telefone` varchar(15) DEFAULT NULL,
  `S_CPFCNPJ` varchar(25) DEFAULT NULL,
  `S_Endereco` varchar(255) DEFAULT NULL,
  `S_Outros` longtext,
  `S_Estado_Civil` varchar(15) DEFAULT NULL,
  `S_IE_RG` varchar(25) DEFAULT NULL,
  `S_Profissao` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`Codigo`),
  KEY `Codigo` (`Codigo`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;

-- Exportação de dados foi desmarcado.

-- Copiando estrutura para tabela simprojeto.contrato
CREATE TABLE IF NOT EXISTS `contrato` (
  `codigo` int(11) NOT NULL AUTO_INCREMENT,
  `S_Contrato` text,
  `S_Nome` varchar(255) DEFAULT NULL,
  `S_CPFCNPJ` varchar(25) DEFAULT NULL,
  `S_Cidade` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`codigo`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

-- Exportação de dados foi desmarcado.

-- Copiando estrutura para tabela simprojeto.orcamento
CREATE TABLE IF NOT EXISTS `orcamento` (
  `Codigo` int(11) NOT NULL AUTO_INCREMENT,
  `S_Nome` varchar(120) DEFAULT NULL,
  `A_Data` datetime DEFAULT NULL,
  `I_Cliente` int(11) DEFAULT '0',
  `S_Nome_Cliente` varchar(120) DEFAULT NULL,
  `S_Outros` longtext,
  `B_Projeto` bit(1) DEFAULT b'0',
  `S_Contrato` longtext,
  PRIMARY KEY (`Codigo`),
  KEY `Codigo` (`Codigo`),
  KEY `ClientesFK` (`I_Cliente`),
  CONSTRAINT `ClientesFK` FOREIGN KEY (`I_Cliente`) REFERENCES `clientes` (`Codigo`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- Exportação de dados foi desmarcado.

-- Copiando estrutura para tabela simprojeto.materiais
CREATE TABLE IF NOT EXISTS `materiais` (
  `Codigo` int(11) NOT NULL AUTO_INCREMENT,
  `I_Orcamento` int(11) NOT NULL DEFAULT '0',
  `S_Material` varchar(255) DEFAULT NULL,
  `D_Valor` decimal(18,2) DEFAULT '0.00',
  `D_Quantidade` decimal(18,2) DEFAULT '0.00',
  `D_ValorTotal` decimal(18,2) DEFAULT '0.00',
  `S_Unidade` varchar(10) DEFAULT NULL,
  PRIMARY KEY (`Codigo`),
  KEY `Codigo` (`Codigo`),
  KEY `OrcamentoFK1` (`I_Orcamento`),
  CONSTRAINT `OrcamentoFK1` FOREIGN KEY (`I_Orcamento`) REFERENCES `orcamento` (`Codigo`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=107 DEFAULT CHARSET=utf8;

-- Exportação de dados foi desmarcado.

-- Copiando estrutura para tabela simprojeto.profissionais
CREATE TABLE IF NOT EXISTS `profissionais` (
  `Codigo` int(11) NOT NULL AUTO_INCREMENT,
  `I_Orcamento` int(11) DEFAULT '0',
  `S_Profissional` varchar(255) DEFAULT NULL,
  `D_Valor` decimal(18,2) DEFAULT '0.00',
  `D_Quantidade` decimal(18,2) DEFAULT '0.00',
  `D_ValorTotal` decimal(18,2) DEFAULT '0.00',
  `S_Tipo` varchar(10) DEFAULT NULL,
  PRIMARY KEY (`Codigo`),
  KEY `Codigo` (`Codigo`),
  KEY `OrcamentoFK` (`I_Orcamento`),
  CONSTRAINT `OrcamentoFK` FOREIGN KEY (`I_Orcamento`) REFERENCES `orcamento` (`Codigo`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;

-- Exportação de dados foi desmarcado.

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
