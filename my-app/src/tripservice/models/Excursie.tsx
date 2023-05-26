/** @format */

export interface Excursie {
	id: string;
	id_obiectiv: string;
	id_firma_transport: string;
	ora: number;
	pret: number;
	nr_locuri_totale: number;
}

export interface ObiectivTuristic {
	id: string;
	nume: string;
}

export interface FirmaTransport {
	id: string;
	nume: string;
}
