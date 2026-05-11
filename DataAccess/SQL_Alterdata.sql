-- Database: Alterdata

-- DROP DATABASE "Alterdata";
/*
CREATE DATABASE "Alterdata"
    WITH 
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'Portuguese_Brazil.1252'
    LC_CTYPE = 'Portuguese_Brazil.1252'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1;
*/

/*
drop table login;
drop table votacao;
drop table recurso;
drop table usuario;
*/

create table usuario (
	id int primary key,
	nome varchar(50) not null,
	email varchar(50) unique not null,
	senha varchar(50) null,
	data_cadastro timestamp default now(),
	ativo bool default true
);

create table login(
	id int primary key,
	usuario_id int not null references usuario(id),
	sessao_id varchar(255) not null,
	data_acesso timestamp default now(),
	ttl timestamp not null
);

create table recurso (
	id int primary key,
	titulo varchar(255) not null,
	comentario text not null,
	data_cadastro timestamp default now(),
	status int default 1 /*1-proposto 2-em_desenvolvimento 3-concluido 4-cancelado*/	
);

create table votacao (
	id int primary key,
	usuario_id int not null references usuario(id),
	recurso_id int not null references recurso(id),
	comentario text not null,
	data_local timestamp default now(),
	data_remota timestamp default now()
);						   

/*
Usuario 1 - Login: agsfmail@gmail.com Senha: 1234 
insert into usuario (id,nome,email,senha,data_cadastro) values (1,'Andre Franco','agsfmail@gmail.com','139F69C93C042496A8E958EC5930662C6CCCAFBF',TIMESTAMP WITH TIME ZONE '2019-02-08 15:00:00+02');
insert into usuario (id,nome,email,senha,data_cadastro) values (2,'Luiz Gustavo','mail@gmail.com','139F69C93C042496A8E958EC5930662C6CCCAFBF',TIMESTAMP '2019-02-08 15:35:00');
insert into usuario (id,nome,email,senha) values (3,'Pedro Cardoso','il@terra.com.br','139F69C93C042496A8E958EC5930662C6CCCAFBF');
insert into usuario (id,nome,email,senha,data_cadastro) values (4,'Luiz Inácio','mail@ig.com.br','139F69C93C042496A8E958EC5930662C6CCCAFBF',TIMESTAMP '2019-02-08 15:35:00');
insert into usuario (id,nome,email,senha,data_cadastro) values (5,'Rodrigo Couto Muzi','mail@msn.com','139F69C93C042496A8E958EC5930662C6CCCAFBF',TIMESTAMP '2019-02-08 15:35:00');
insert into recurso values (1,'Ordenação na tela de produtos.','Correção da ordenação ao se clicar no topo da coluna na tela de produtos',now(),1);
insert into recurso values (2,'Erro ao inserir dependentes.','Ao inserir dependentes o sistema retorna um erro.',now(),4);
insert into recurso values (3,'Campo recuperar senha.','Colocar um campo para recuperar senha na tela de login.',now(),2);
insert into recurso values (4,'Erro exclusão de receitas.','Ocorre um erro ao excluir receitas de determinados produtos.',now(),1);
insert into recurso values (5,'Email não enviado na tela de compras.','Ao finalizar um pedido o email de confirmação nao é enviado ao fornecedor.',now(),1);
insert into recurso values (6,'Menssagem entre usuários.','Usuário com permissão não consegue enviar menssagem para outros usuários.',now(),3);
insert into votacao values(3,3,1,'Sem ordenação o usuário não consegue trabalhar.',now(),now());
insert into votacao values(4,2,4,'Atrapalha a produção.',now(),now());
insert into votacao values(5,3,4,'Atrapalha a produção.',now(),now());
insert into votacao values(6,2,1,'A ordenação é importante.',now(),now());
insert into votacao values(7,3,2,'A produção é importante.',now(),now());
insert into votacao values(8,3,2,'A produção é importante.',now(),now());
insert into votacao values(9,4,4,'Acredito ser importante por atrapalhar a produção.',now(),now());
insert into votacao values(10,5,4,'Atrapalha a produção.',now(),now());
insert into votacao values(11,4,1,'A ordenação é importante.',now(),now());

select * from usuario; 
select * from login;
select * from recurso;
select * from votacao;

delete from login;
delete from votacao;
delete from usuario;
delete from recurso;
*/