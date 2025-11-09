import { useState, useEffect } from 'react';
import { Table, Button, Alert, Spinner } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import api from '../services/api';

export default function HospitalList() {
  const [hospitals, setHospitals] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const navigate = useNavigate();

  useEffect(() => {
    fetchData();
  }, []);

  const fetchData = async () => {
    try {
      const res = await api.get('/hospitals');
      setHospitals(res.data);
    } catch (err) {
      setError('Không tải được danh sách bệnh viện');
    } finally {
      setLoading(false);
    }
  };

  const handleDelete = async (id) => {
    if (window.confirm('Xóa bệnh viện này?')) {
      await api.delete(`/hospitals/${id}`);
      fetchData();
    }
  };

  if (loading) return <Spinner animation="border" className="d-block mx-auto mt-5" />;
  if (error) return <Alert variant="danger">{error}</Alert>;

  return (
    <>
      <div className="d-flex justify-content-between align-items-center mb-3">
        <h2>Danh sách Bệnh viện</h2>
        <Button variant="success" onClick={() => navigate('/hospitals/new')}>
          Thêm Bệnh viện
        </Button>
      </div>

      <Table striped bordered hover responsive>
        <thead className="table-dark">
          <tr>
            <th>Tên bệnh viện</th>
            <th>Địa chỉ / Vị trí</th>
            <th>Hành động</th>
          </tr>
        </thead>
        <tbody>
          {hospitals.map(h => (
            <tr key={h.hospitalId}>
              <td>{h.name}</td>
              <td>{h.location}</td>
              <td>
                <Button
                  size="sm"
                  variant="warning"
                  className="me-2"
                  onClick={() => navigate(`/hospitals/edit/${h.hospitalId}`)}
                >
                  Sửa
                </Button>
                <Button
                  size="sm"
                  variant="danger"
                  onClick={() => handleDelete(h.hospitalId)}
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
