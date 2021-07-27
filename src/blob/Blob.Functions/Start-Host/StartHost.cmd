
set curr_dir=%~dp0
set curr_drive=%curr_dir:~0,2%
%curr_drive%
cd %curr_dir%
cd ..\

func host start --useHttps --cert Certs\dev.mysundial.com.pfx --password MySundial1234 --verbose
Exit 0