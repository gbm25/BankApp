INSERT INTO bankappdb.dbo.Customer (first_name,last_name,username,password,country,region,city,address,last_update)
VALUES ('Pepe','Gonzalez','PeGon','Abc123..','España','Galicia','A Coruña','Calle de la Republica nº3 bajo 2',NULL),
('Jose Antonio','Fernandez','JoFer','Contraseña1234!','España','Galicia','A Coruña','Calle Gran via nº14 portal 3 5I',NULL),
('Manuel','Prado','MaPra','grteFGYawet67','España','Castilla y león','Valladolid','Plaza España nº15',NULL);

INSERT INTO bankappdb.dbo.Account (customer_id,account_number,description)
VALUES (1,'ES6621000418401234567891','Cuenta personal'),
(1,'ES6000491500051234567892','Cuenta de trabajo'),
(1,'ES9420805801101234567891','Cuenta especial transacciones'),
(1,'ES9000246912501234567891','No usar esta cuenta sin aprovación de la directiva'),
(1,'ES7100302053091234567895','Cuenta control'),
(1,'ES1000492352082414205416','Cuenta relativa 3% de interés'),
(1,'ES1720852066623456789011','23ES54'),
(2,'AT611903310234573201','Cuenta personal'),
(2,'AT621903310234573202','Cuenta de trabajo'),
(2,'AT631903310234573203','Cuenta especial pagarés'),
(2,'AT641903310234573204','No usar esta cuenta sin aprovación de la comisión temporal empresarial'),
(2,'AT651903310234573205','Cuenta control'),
(2,'AT611903310234573206','Cuenta relativa 1.5% de interés'),
(2,'AT621903310234573207','Codigo 65-21-78'),
(3,'BE62510007547061','Cuenta pagos CEO'),
(3,'BE63510227547062','Cuenta tramites BE'),
(3,'BE65510547547063','Cuenta especial transacciones'),
(3,'BE65510667547064','No usar esta cuenta sin aprovación de la directiva'),
(3,'BE62510007547065','Cuenta control'),
(3,'BE62515707547066','Cuenta matriz ext.'),
(3,'BE62517607547067','23ES54');