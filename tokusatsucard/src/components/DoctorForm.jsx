import { useState, useEffect } from 'react';
import { Form, Button, Alert, Card } from 'react-bootstrap';
import { useParams, useNavigate } from 'react-router-dom';
import api from '../services/api';

export default function DoctorForm() {
  const { id } = useParams();
  const navigate = useNavigate();
  const isEdit = Boolean(id);

  const [form, setForm] = useState({ name: '', specialization: '', hospitalId: '' });
  const [hospitals, setHospitals] = useState([]);
  const [error, setError] = useState('');
  const [validated, setValidated] = useState(false);

  useEffect(() => {
    api.get('/hospitals').then(res => setHospitals(res.data))
       .catch(() => setError('Không tải được danh sách bệnh viện'));

    if (isEdit) {
      api.get(`/doctors/${id}`).then(res => setForm(res.data))
         .catch(() => setError('Không tải được thông tin bác sĩ'));
    }
  }, [id, isEdit]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    const formEl = e.currentTarget;
    if (formEl.checkValidity() === false) {
      e.stopPropagation();
      setValidated(true);
      return;
    }

    try {
      if (isEdit) {
        await api.put(`/doctors/${id}`, { ...form, doctorId: id });
      } else {
        await api.post('/doctors', form);
      }
      navigate('/doctors');
    } catch (err) {
      setError(err.response?.data || 'Lỗi server');
    }
  };

  return (
    <Card className="mx-auto mt-4" style={{ maxWidth: '600px' }}>
      <Card.Header>
        <h4>{isEdit ? 'Sửa' : 'Thêm'} Bác sĩ</h4>
      </Card.Header>
      <Card.Body>
        {error && <Alert variant="danger">{error}</Alert>}
        <Form noValidate validated={validated} onSubmit={handleSubmit}>
          <Form.Group className="mb-3">
            <Form.Label>Tên đầy đủ</Form.Label>
            <Form.Control
              required
              type="text"
              value={form.name || ''}
              onChange={e => setForm({ ...form, name: e.target.value })}
            />
            <Form.Control.Feedback type="invalid">
              Vui lòng nhập tên
            </Form.Control.Feedback>
          </Form.Group>

          <Form.Group className="mb-3">
            <Form.Label>Chuyên khoa</Form.Label>
            <Form.Control
              required
              type="text"
              value={form.specialization || ''}
              onChange={e => setForm({ ...form, specialization: e.target.value })}
            />
          </Form.Group>

          <Form.Group className="mb-3">
            <Form.Label>Bệnh viện</Form.Label>
            <Form.Select
              required
              value={form.hospitalId || ''}
              onChange={e => setForm({ ...form, hospitalId: parseInt(e.target.value) })}
            >
              <option value="">-- Chọn bệnh viện --</option>
              {hospitals.map(h => (
                <option key={h.hospitalId} value={h.hospitalId}>
                  {h.name}
                </option>
              ))}
            </Form.Select>
          </Form.Group>

          <div className="d-flex gap-2">
            <Button variant="primary" type="submit">
              {isEdit ? 'Cập nhật' : 'Tạo mới'}
            </Button>
            <Button variant="secondary" onClick={() => navigate('/doctors')}>
              Hủy
            </Button>
          </div>
        </Form>
      </Card.Body>
    </Card>
  );
}
