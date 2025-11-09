import { useState, useEffect } from 'react';
import { Table, Button, Alert, Spinner } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import api from '../services/api';

export default function NurseList() {
  const [nurses, setNurses] = useState([]);
  const [wards, setWards] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const navigate = useNavigate();

  useEffect(() => {
    fetchData();
  }, []);

  const fetchData = async () => {
    try {
      const [nurseRes, wardRes] = await Promise.all([
        api.get('/nurses'),
        api.get('/wards')
      ]);
      setNurses(nurseRes.data);
      setWards(wardRes.data);
    } catch (err) {
      setError('Không tải được dữ liệu');
    } finally {
      setLoading(false);
    }
  };

  const handleDelete = async (id) => {
    if (window.confirm('Xóa y tá này?')) {
      await api.delete(`/nurses/${id}`);
      fetchData();
    }
  };

  const getWardName = (wardId) => {
    const ward = wards.find(w => w.wardId === wardId);
    return ward ? ward.name : 'Không xác định';
  };

  if (loading) return <Spinner animation="border" className="d-block mx-auto mt-5" />;
  if (error) return <Alert variant="danger">{error}</Alert>;

  return (
    <>
      <div className="d-flex justify-content-between align-items-center mb-3">
        <h2>Danh sách Y tá</h2>
        <Button variant="success" onClick={() => navigate('/nurses/new')}>
          Thêm Y tá
        </Button>
      </div>

      <Table striped bordered hover responsive>
        <thead className="table-dark">
          <tr>
            <th>Tên</th>
            <th>Chứng chỉ</th>
            <th>Khoa</th>
            <th>Hành động</th>
          </tr>
        </thead>
        <tbody>
          {nurses.map(n => (
            <tr key={n.nurseId}>
              <td>{n.name}</td>
              <td>{n.certification}</td>
              <td>{getWardName(n.wardId)}</td>
              <td>
                <Button size="sm" variant="warning" className="me-2"
                  onClick={() => navigate(`/nurses/edit/${n.nurseId}`)}>
                  Sửa
                </Button>
                <Button size="sm" variant="danger"
                  onClick={() => handleDelete(n.nurseId)}>
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