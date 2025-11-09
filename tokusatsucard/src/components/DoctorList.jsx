import { useState, useEffect } from 'react';
import { Table, Button, Alert, Spinner } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import api from '../services/api';

export default function DoctorList() {
  const [doctors, setDoctors] = useState([]);
  const [hospitals, setHospitals] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const navigate = useNavigate();

  useEffect(() => {
    fetchData();
  }, []);

  const fetchData = async () => {
    try {
      const [doctorRes, hospitalRes] = await Promise.all([
        api.get('/doctors'),
        api.get('/hospitals')
      ]);
      setDoctors(doctorRes.data);
      setHospitals(hospitalRes.data);
    } catch (err) {
      setError('Không tải được dữ liệu');
    } finally {
      setLoading(false);
    }
  };

  const handleDelete = async (id) => {
    if (window.confirm('Xóa bác sĩ này?')) {
      await api.delete(`/doctors/${id}`);
      fetchData();
    }
  };

  const getHospitalName = (hospitalId) => {
    const h = hospitals.find(h => h.hospitalId === hospitalId);
    return h ? h.name : 'Không xác định';
  };

  if (loading) return <Spinner animation="border" className="d-block mx-auto mt-5" />;
  if (error) return <Alert variant="danger">{error}</Alert>;

  return (
    <>
      <div className="d-flex justify-content-between align-items-center mb-3">
        <h2>Danh sách Bác sĩ</h2>
        <Button variant="success" onClick={() => navigate('/doctors/new')}>
          Thêm Bác sĩ
        </Button>
      </div>

      <Table striped bordered hover responsive>
        <thead className="table-dark">
          <tr>
            <th>Tên</th>
            <th>Chuyên khoa</th>
            <th>Bệnh viện</th>
            <th>Hành động</th>
          </tr>
        </thead>
        <tbody>
          {doctors.map(d => (
            <tr key={d.doctorId}>
              <td>{d.name}</td>
              <td>{d.specialization}</td>
              <td>{getHospitalName(d.hospitalId)}</td>
              <td>
                <Button
                  size="sm"
                  variant="warning"
                  className="me-2"
                  onClick={() => navigate(`/doctors/edit/${d.doctorId}`)}
                >
                  Sửa
                </Button>
                <Button
                  size="sm"
                  variant="danger"
                  onClick={() => handleDelete(d.doctorId)}
                >
                  Xóa
                </Button>
              </td>
            </tr>
          ))}
        </tbody>
      </Table>
    </>
  );
}
