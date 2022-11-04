# Linkfolio

#Instalação

Instalar Visual Studio 2022 Comunidade
Marcar ASP.NET e desenvolvimento Web e Desenvolvimento para desktop com .net
https://visualstudio.microsoft.com/pt-br/downloads/

Instalar MongoDB Version 5.0.13
https://www.mongodb.com/try/download/community

Instalar 3T Studio
https://studio3t.com/

#Conexões

Será necessário conectar no banco de dados local.
Sendo assim, deverá se conectar nos programas Studio 3t e no MongoDB.

#Github

Os arquivos de código são encontrados no seguinte repositório:
https://github.com/PabloQuadros/Linkfolio

#Execução do código

Deverá ser aberto o arquivo LinkfolioApi.sln, utilizando da IDE Visual Studio.
Logo após, o código poderá ser executado normalmente.
Após a execução, será aberto uma nova guia no navegador.
Caso o modelo de inicialização não seja o linkfolio.business, será necessário entrar na pasta business, clicar com botão direito na solução linkfolio.business, e clicar em definir como projeto de inicialização.

#Link no navegador

Serão abertos 15 abas expandiveis divididas em 3 CRUD's diferentes, Login, Message, e Portifolio.
Ao clicar em qualquer uma das abas fará com que ela se expanda revelando as informações necessárias para a sua utilização.

Apenas a aba CreateLogin e Login funcionam sem autenticação.
Após criar uma conta é necessário ir na rota Login e realizar o login, recebendo uma token de autenticação.
Assim todas as outras rotas ficam livres para serem utilizadas.

