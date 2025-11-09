import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Navbar from './components/Navbar';
import NurseList from './components/NurseList';
import NurseForm from './components/NurseForm';
import WardList from './components/WardList';
import WardForm from './components/WardForm';

function App() {
  return (
    <BrowserRouter>
      <Navbar />
      <div className="container mt-4">
        <Routes>
          <Route path="/" element={<h1 className="text-center">Hospital Management System</h1>} />
          <Route path="/nurses" element={<NurseList />} />
          <Route path="/nurses/new" element={<NurseForm />} />
          <Route path="/nurses/edit/:id" element={<NurseForm />} />
          <Route path="/wards" element={<WardList />} />
          <Route path="/wards/new" element={<WardForm />} />
          <Route path="/wards/edit/:id" element={<WardForm />} />
        </Routes>
      </div>
    </BrowserRouter>
  );
}

export default App;