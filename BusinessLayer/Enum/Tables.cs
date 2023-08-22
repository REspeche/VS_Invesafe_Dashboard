namespace BusinessLayer.Enum
{
    public static class Tables
    {
        public enum yesNo
        {
            No = 0,
            Yes = 1
        }

        public enum gender
        {
            Undefined = 0,
            Male = 1,
            Female = 2,
            Other = 3
        }

        public enum language
        {
            Spanish = 1
        }

        public enum country
        {
            Argentina = 10
        }

        public enum statusProject
        {
            Financing = 1, //En financiación
            Funded = 2 //Financiado
        }

        public enum typeProject
        {
            FixedInterest = 1, //Interes Fijo
            Investment = 2 //Inversión
        }

        public enum alertState
        {
            Pendient = 1, //Pendiente
            Sent = 2, //Enviado
            Rejected = 3, //Rechazado
            Approved = 4 //Aprobado
        }

        public enum ioMovement
        {
            Input = 1,
            Output = 2
        }

        public enum paymentMethod
        {
            Card = 1,
            Account = 2
        }

        public enum typeOperation
        {
            Beneficio_de_alquiler = 1,
            Beneficio_por_venta = 2,
            Comisión = 3,
            Comision_Tarjeta = 4,
            Comision_Marketplace = 5,
            Comision_Compra_Marketplace = 6,
            Compra_fracciones = 7,
            Devolucion_de_capital = 8,
            Inversion = 9,
            Pago_con_tarjeta = 10,
            Transferencia_Entrante = 11,
            Transferencia_Saliente = 12,
            Venta_fracciones = 13,
            Promociones = 14
        }

        public enum typeExpense
        {
            Comision_Invesafe = 1,
            Gastos_Extraordinarios = 2,
            Pérdidas = 3
        }

        public enum typeBenefit
        {
            Beneficios = 1,
            Beneficios_extra = 2,
            Beneficio_de_venta = 3,
            Beneficio_por_intereses = 4,
            Beneficio_por_compra_de_acciones = 5
        }

        public enum typeInvest
        {
            No_Acreditado = 1,
            Acreditado = 2
        }

        public enum InvestQuestion1
        {
            Ingresos_Mas_50_Mil = 1, //Tengo unos ingresos anuales de más de $ 50,000
            Ingresos_Mas_100_Mil = 2, //Tengo unos ingresos anuales de más de $ 100,000
            Recibiendo_Asesoramiento_Profesional = 3 //Estoy recibiendo asesoramiento profesional sobre este tipo de inversiones
        }

        public enum InvestQuestion2
        {
            Fondos_De_Inversion = 1,
            Valores_Renta_Fija = 2,
            Valores_Renta_Variable = 3,
            Instrumentos_Derivados = 4,
            Otros_Instrumentos_Financieros = 5
        }

        public enum Notifications
        {
            Acredite_Fondos = 1,
            Valide_Cuenta_Bancaria = 2,
            Valide_Identidad = 3,
            Patrimonio_Actualizado = 4,
            Transfencia_Cuenta_Bancaria = 5,
            Comprar_Inversion = 6,
            Nuevo_Proyecto = 7,
            Proyecto_Alcance_Objetivo = 8,
            Proyecto_Finalice = 9
        }
    }
}
