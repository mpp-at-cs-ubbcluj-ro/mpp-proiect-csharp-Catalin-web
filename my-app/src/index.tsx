/** @format */
// App.tsx base page:
import { createRoot } from 'react-dom/client';
import { TablePage } from './tablePage';

const container = document.getElementById('root'); //ia div-ul root din index.html

if (!container) {
	throw new Error("React root element doesn't exist!");
}

const root = createRoot(container);

root.render(
	<div>
		<TablePage />
	</div>,
);
